
namespace InvestigacionMAUI_ListaDeTareas;

public partial class MainPage : ContentPage
{
    private int tareasPendientes = 0;

    public MainPage()
    {
        InitializeComponent();
    }
    //MAUI utiliza C# y XAML, Flutter usa Dart, React Native usa JavaScript/TypeScript y 
    //Kotlin Multiplaform usa kotlin para compartir codigo.
    private void OnAgregarClicked(object sender, EventArgs e)
    {
        string texto = TareaEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(texto))
        {
            MensajeLabel.Text = "Escribe una tarea primero.";
            return;
        }

        var tareaLayout = new HorizontalStackLayout
        {
            Spacing = 10,
            Padding = new Thickness(12),
            BackgroundColor = Colors.White
        };

        var tareaLabel = new Label
        {
            Text = texto,
            FontSize = 16,
            TextColor = Color.FromArgb("#173B70"),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Fill
        };

        var completarBtn = new Button
        {
            Text = "✓",
            FontSize = 18,
            BackgroundColor = Color.FromArgb("#DCFCE7"),
            TextColor = Color.FromArgb("#166534"),
            Padding = new Thickness(10, 4),
            CornerRadius = 8
        };

        // La lógica de la aplicación se programa en C# en MAUI;
        // las otras tecnologías usan Dart, JavaScript/TypeScript o Kotlin.
        completarBtn.Clicked += (s, args) =>
        {
            if (tareaLayout.Children.Contains(tareaLabel))
            {
                tareaLabel.TextDecorations = TextDecorations.Strikethrough;
                tareaLabel.TextColor = Colors.Gray;
                completarBtn.IsEnabled = false;
                tareasPendientes--;
                ActualizarContador();
                MensajeLabel.Text = "¡Tarea completada!";
            }
        };


        tareaLayout.Children.Add(tareaLabel);
        tareaLayout.Children.Add(completarBtn);

        var eliminarBtn = new Button
        {
            Text = "X",
            FontSize = 16,
            BackgroundColor = Color.FromArgb("#FEE2E2"),
            TextColor = Color.FromArgb("#B91C1C"),
            Padding = new Thickness(10, 4),
            CornerRadius = 8
        };

        eliminarBtn.Clicked += (s, args) =>
        {
            if (tareaLayout.Parent == ListaTareas)
            {
                if (completarBtn.IsEnabled)
                {
                    tareasPendientes--;
                    ActualizarContador();
                }

                ListaTareas.Children.Remove(tareaLayout);
                MensajeLabel.Text = "Tarea eliminada.";
            }
        };

        tareaLayout.Children.Add(eliminarBtn);
        ListaTareas.Children.Add(tareaLayout);


        tareasPendientes++;
        ActualizarContador();

        TareaEntry.Text = "";
        MensajeLabel.Text = "Tarea agregada correctamente.";
    }

    private void ActualizarContador()
    {
        ContadorLabel.Text = $"Tareas pendientes: {tareasPendientes}";
    }
}
