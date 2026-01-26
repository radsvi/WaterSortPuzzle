using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColN = WaterSortPuzzle.Enums.LiquidColorName; // creating alias so that I dont have to have long names in GenerateDebugLevel();


namespace WaterSortPuzzle.ViewModels
{
    public partial class HelpPopupVM : FullscreenParameterlessPopupBaseVM
    {
        private readonly BoardState boardState;
        [ObservableProperty]
        private ObservableCollection<TubeData> tubesItemsSource = new ObservableCollection<TubeData>();

        public HelpPopupVM(IPopupService popupService, MainVM mainVM, BoardState boardState) : base(popupService, mainVM)
        {

            var newTubeItems = new ObservableCollection<TubeData>();
            //for (int x = 0; x < 3; x++)
            //{
            //    //TubesItemsSource.Add(new TubeData(GameState[x, 0], GameState[x, 1], GameState[x, 2], GameState[x, 3]));
            //    newTubeItems.Add(new TubeData(GameState.BoardState.Grid, x));
            //}
            int i = 0;
            var board = boardState.FactoryCreate(new LiquidColor[Constants.ColorCount + 10, Constants.Layers]);

            board.AddStartingTube(i++, new ColN[] { ColN.Pink, ColN.Brown, ColN.Pink, ColN.Indigo });
            board.AddStartingTube(i++, new ColN[] { ColN.Green, ColN.Brown, ColN.Indigo, ColN.Purple });


            TubesItemsSource = newTubeItems;
            OnPropertyChanged(nameof(TubesItemsSource));
            //this.boardState = boardState;
        }
    }
}
