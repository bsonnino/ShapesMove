using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ShapesMove
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point start; // Ponto base para a movimentação
        int currentZ; // Z-Index atual
        bool isDragging; // Está movendo?
        Shape movedElement; // Elemento sendo movido

        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvas1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {           
            // Verifica se clicamos numa Shape
            if (e.Source is Shape)
            {
                // Pega posição atual do mouse
                start = e.GetPosition(canvas1);
                // Inicializa variáveis e configura opacidade da shape para 0.5
                isDragging = true;
                movedElement = (Shape)e.Source;
                ((Shape)e.Source).Opacity = 0.5;
                canvas1.CaptureMouse();
                e.Handled = true;
            }
        }

        private void canvas1_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point pt = e.GetPosition(canvas1);
                // Pega posição atual da shape
                double currentLeft = (double)movedElement.GetValue(Canvas.LeftProperty);
                double currentTop = (double)movedElement.GetValue(Canvas.TopProperty);

                // Calcula nova posição
                double newLeft = currentLeft + pt.X - start.X;
                double newTop = currentTop + pt.Y - start.Y;

                // Reposiciona elemento
                movedElement.SetValue(Canvas.LeftProperty, newLeft);
                movedElement.SetValue(Canvas.TopProperty, newTop);

                start = pt;
                e.Handled = true;
            }
        }

        private void canvas1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Restaura valores
            movedElement.Opacity = 1;
            movedElement.SetValue(Canvas.ZIndexProperty, ++currentZ);
            isDragging = false;
            canvas1.ReleaseMouseCapture();
        }

    }
}
