﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Esri.Calcite.WPF.Helpers
{
    public static class GridHelpers
    {
        private static readonly DependencyProperty TextColumnStyleProperty =
            DependencyProperty.RegisterAttached("TextColumnStyle", typeof(Style), typeof(GridHelpers), new PropertyMetadata { PropertyChangedCallback = HandlePropertyChangeCallbck });

        private static readonly DependencyProperty CheckBoxColumnStyleProperty =
            DependencyProperty.RegisterAttached("CheckBoxColumnStyle", typeof(Style), typeof(GridHelpers), new PropertyMetadata { PropertyChangedCallback = HandlePropertyChangeCallbck });

        private static void HandlePropertyChangeCallbck(object obj, DependencyPropertyChangedEventArgs e)
        {
            var grid = (DataGrid)obj;
            if (e.OldValue == null && e.NewValue != null)
                grid.Columns.CollectionChanged += (obj2, e2) =>
                {
                    UpdateColumnStyles(grid);
                };
            UpdateColumnStyles(grid);
        }

        public static void SetTextColumnStyle(DependencyObject element, Style value) => element.SetValue(TextColumnStyleProperty, value);
        public static Style? GetTextColumnStyle(DependencyObject element) => element.GetValue(TextColumnStyleProperty) as Style;

        public static void SetCheckBoxColumnStyle(DependencyObject element, Style value) => element.SetValue(CheckBoxColumnStyleProperty, value);
        public static Style? GetCheckBoxColumnStyle(DependencyObject element) => element.GetValue(CheckBoxColumnStyleProperty) as Style;

        private static void UpdateColumnStyles(DataGrid grid)
        {
            foreach (var column in grid.Columns)
            {
                if (column is DataGridTextColumn tc)
                {
                    tc.ElementStyle = GetTextColumnStyle(grid);
                }
                else if (column is DataGridCheckBoxColumn cc)
                {
                    cc.ElementStyle = GetCheckBoxColumnStyle(grid);
                }
            }
        }
    }
}
