using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace TaxCalculator.Desktop.Controls;

public class LabeledTextBox : TemplatedControl
{
    private string _text;

    private string _label;

    private TextWrapping _textWrapping;

    private bool _acceptReturns;
    
    public string Text
    {
        get => _text;
        set => SetAndRaise(TextProperty, ref _text, value);
    }

    public string Label
    {
        get => _label;
        set => SetAndRaise(LabelProperty, ref _label, value);
    }

    public TextWrapping TextWrapping
    {
        get => _textWrapping;
        set => SetAndRaise(TextWrappingProperty, ref _textWrapping, value);
    }

    public bool AcceptReturns
    {
        get => _acceptReturns;
        set => SetAndRaise(AcceptReturnsProperty, ref _acceptReturns, value);
    }

    public static readonly DirectProperty<LabeledTextBox, string> TextProperty =
        AvaloniaProperty.RegisterDirect<LabeledTextBox, string>(nameof(Text), textBox => textBox.Text,
            (textBox, value) => textBox.Text = value);

    public static readonly DirectProperty<LabeledTextBox, string> LabelProperty =
        AvaloniaProperty.RegisterDirect<LabeledTextBox, string>(nameof(Label), textBox => textBox.Label,
            (textBox, value) => textBox.Label = value);

    public static readonly DirectProperty<LabeledTextBox, TextWrapping> TextWrappingProperty =
        AvaloniaProperty.RegisterDirect<LabeledTextBox, TextWrapping>(nameof(TextWrapping),
            textBox => textBox.TextWrapping,
            (textBox, value) => textBox.TextWrapping = value);

    public static readonly DirectProperty<LabeledTextBox, bool> AcceptReturnsProperty =
        AvaloniaProperty.RegisterDirect<LabeledTextBox, bool>(nameof(AcceptReturns), textBox => textBox.AcceptReturns,
            (textBox, value) => textBox.AcceptReturns = value);
}