﻿using System.Globalization;

namespace WaterSortPuzzle.Converters
{
    class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //if (values.Length > 1)
            //{
            //    Tube tube = (Tube)values[0];
            //    tube.TubeButton = (Button)values[1];
            //    return tube;
            //}

            //if (values.Length > 0)
            //{
            //    TubeButton obj = new TubeButton();
            //    foreach (var item in values)
            //    {
            //        obj.Contents.Add(item);
            //    }

            //    return obj;
            //}
            if (values.Length > 0)
            {
                TubeReference obj = new TubeReference
                (
                    (TubeControl)values[0],
                    (Button)values[1],
                    ((TubeControl)values[0]).TubeId,
                    (Grid)values[2]
                );


                return obj;
            }

            //if (values.Length > 1)
            //{
            //    return new TubeButton { Tube = (Tube)values[0], ButtonElement = (Button)values[1] };
            //}
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
