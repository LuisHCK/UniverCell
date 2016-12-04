using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniverCell
{
    public partial class MainWindow
    {
        private void check_bx_repuesto_Checked(object sender, RoutedEventArgs e)
        {

            lbl_precio_rep.IsEnabled = true;
            txt_box_rep_precio.IsEnabled = true;
            lbl_descr_rep.IsEnabled = true;
            txt_box_desc_repuesto.IsEnabled = true;
        }

        private void check_bx_repuesto_Unchecked(object sender, RoutedEventArgs e)
        {
            lbl_precio_rep.IsEnabled = false;
            txt_box_rep_precio.IsEnabled = false;
            lbl_descr_rep.IsEnabled = false;
            txt_box_desc_repuesto.IsEnabled = false;

        }
    }
}
