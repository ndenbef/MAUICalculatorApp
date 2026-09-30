using System.Globalization;

namespace MauiApp260926;

public partial class MainPage : ContentPage
{
    private double _firstNumber = 0;
    private double _secondNumber = 0;
    private string _selectedOperator = string.Empty;
    private bool _isNewEntry = true;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnNumberClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        string pressed = button.Text;

        if (ResultLabel.Text == "0" || _isNewEntry)
        {
            ResultLabel.Text = pressed;
            _isNewEntry = false;
        }
        else
        {
            ResultLabel.Text += pressed;
        }
    }

    private void OnDecimalClicked(object sender, EventArgs e)
    {
        if (_isNewEntry)
        {
            ResultLabel.Text = "0,";
            _isNewEntry = false;
        }
        else if (!ResultLabel.Text.Contains(","))
        {
            ResultLabel.Text += ",";
        }
    }

    private void OnOperatorClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;

        if (double.TryParse(ResultLabel.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
        {
            _firstNumber = number;
            _selectedOperator = button.Text;
            CurrentCalculationLabel.Text = $"{_firstNumber} {_selectedOperator}";
            _isNewEntry = true;
        }
    }

    private void OnEqualClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedOperator)) return;

        if (double.TryParse(ResultLabel.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
        {
            _secondNumber = number;
            double result = Calculate(_firstNumber, _secondNumber, _selectedOperator);

            CurrentCalculationLabel.Text = $"{_firstNumber} {_selectedOperator} {_secondNumber} =";
            ResultLabel.Text = result.ToString(CultureInfo.CurrentCulture);

            _firstNumber = result;
            _selectedOperator = string.Empty;
            _isNewEntry = true;
        }
    }

    private double Calculate(double val1, double val2, string op)
    {
        return op switch
        {
            "+" => val1 + val2,
            "-" => val1 - val2,
            "×" => val1 * val2,
            "÷" => val2 != 0 ? val1 / val2 : double.NaN,
            _ => 0
        };
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        _firstNumber = 0;
        _secondNumber = 0;
        _selectedOperator = string.Empty;
        _isNewEntry = true;
        ResultLabel.Text = "0";
        CurrentCalculationLabel.Text = string.Empty;
    }

    private void OnNegateClicked(object sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
        {
            number = -number;
            ResultLabel.Text = number.ToString(CultureInfo.CurrentCulture);
        }
    }

    private void OnPercentageClicked(object sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
        {
            number /= 100;
            ResultLabel.Text = number.ToString(CultureInfo.CurrentCulture);
            _isNewEntry = true;
        }
    }
}