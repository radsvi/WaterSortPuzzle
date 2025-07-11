using Android.Views;
using AndroidX.RecyclerView.Widget;
using Microsoft.Maui.Controls.Handlers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Handlers;

namespace WaterSortPuzzle.Platforms.Android
{
    public class CustomCollectionViewHandler : CollectionViewHandler
    {
        // We don't override the constructor anymore

        public static void MapItemsSource(CollectionViewHandler handler, Microsoft.Maui.Controls.CollectionView collectionView)
        {
            // First call default behavior
            CollectionViewHandler.Mapper.ModifyMapping(
                nameof(Microsoft.Maui.Controls.CollectionView.ItemsSource),
                (h, v, action) =>
                {
                    action?.Invoke(h, v); // call the default mapping

                    if (h.PlatformView is RecyclerView recyclerView)
                    {
                        recyclerView.OverScrollMode = OverScrollMode.Never;
                    }
                });
        }
    }
}
