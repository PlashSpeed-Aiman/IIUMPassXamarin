using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IIUMPassXamarin.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpandableView : ContentView
    {
        private TapGestureRecognizer _tapGestureRecognizer;
        private StackLayout _summary;
        private StackLayout _details;
        
        
        
        public ExpandableView()
        {
            InitializeComponent();
            InitializeGuestureRecognizer();
            SubscribeToGuestureHandler();    
        }
        public virtual StackLayout Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;    
                SummaryRegion.Children.Add(_summary);
                OnPropertyChanged();
            }
        }

        public virtual StackLayout Details
        {
            get { return _details; }
            set 
            {
                _details = value;
                DetailsRegion.Children.Add(_details);
                OnPropertyChanged();
            }
        }
        private void InitializeGuestureRecognizer()
        {
            _tapGestureRecognizer= new TapGestureRecognizer();
            SummaryRegion.GestureRecognizers.Add(_tapGestureRecognizer);
        }
        
        private void SubscribeToGuestureHandler()
        {
            _tapGestureRecognizer.Tapped += TapRecogniser_Tapped;
        }

        private void TapRecogniser_Tapped(object sender, EventArgs e)
        {
            if (DetailsRegion.IsVisible)
            {
                DetailsRegion.IsVisible = false;
            }
            else
            {
                DetailsRegion.IsVisible = true;
                
            }
        }
    }
}