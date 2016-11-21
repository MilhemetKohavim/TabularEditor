﻿using Aga.Controls.Tree;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabularEditor.TOMWrapper;
using TOM = Microsoft.AnalysisServices.Tabular;

namespace TabularEditor.UI.Tree
{
    public class TabularIcon : Aga.Controls.Tree.NodeControls.NodeIcon
    {
        public readonly Dictionary<ObjectType, int> IconMap = new Dictionary<ObjectType, int>()
        {
            { ObjectType.Table, TabularIcons.ICON_TABLE },
            { ObjectType.Hierarchy, TabularIcons.ICON_HIERARCHY },
            { ObjectType.Column, TabularIcons.ICON_COLUMN },
            { ObjectType.Measure, TabularIcons.ICON_MEASURE },
            { ObjectType.KPI, TabularIcons.ICON_KPI },
            { ObjectType.Model, TabularIcons.ICON_CUBE },
            { ObjectType.Level, TabularIcons.ICON_LEVEL1 }
        };


        public System.Windows.Forms.ImageList.ImageCollection Images { get; set; }

        protected override Image GetIcon(TreeNodeAdv node)
        {
            if (node.Tag is Folder)
            {
                return Images[node.IsExpanded ? TabularIcons.ICON_FOLDEROPEN : TabularIcons.ICON_FOLDER];
            }
            else if (node.Tag is TabularObject)
            {
                if (node.Tag is CalculatedColumn) return Images[TabularIcons.ICON_CALCCOLUMN];
                if (node.Tag is CalculatedTable) return Images[TabularIcons.ICON_CALCTABLE];
                if (node.Tag is Level) return Images[TabularIcons.ICON_LEVEL1 + (node.Tag as Level).Ordinal];

                int iconIndex;
                if (IconMap.TryGetValue((node.Tag as TabularObject).ObjectType, out iconIndex)) return Images[iconIndex];
            }

            return base.GetIcon(node);
        }

        public override void Draw(TreeNodeAdv node, DrawContext context)
        {
            base.Draw(node, context);

            var err = (node.Tag as IErrorMessageObject)?.ErrorMessage;
            var needsVal = (node.Tag as IExpressionObject)?.NeedsValidation ?? false;
            if (needsVal)
            {
                context.Graphics.DrawImage(Images[TabularIcons.ICON_QUESTION], 4 + context.Bounds.Left, 3 + context.Bounds.Top);
            }
            else if (!string.IsNullOrEmpty(err))
            {
                context.Graphics.DrawImage(Images[TabularIcons.ICON_WARNING], 3 + context.Bounds.Left, 3 + context.Bounds.Top);
            }
        }

        public override string GetToolTip(TreeNodeAdv node)
        {
            var err = (node.Tag as IErrorMessageObject)?.ErrorMessage;
            var needsVal = (node.Tag as IExpressionObject)?.NeedsValidation ?? false;
            return needsVal ? "Expression was changed. Deploy to validate." : err ?? string.Empty;
        }
    }


    static class TabularIcons
    {
        public const int ICON_FOLDER = 0;
        public const int ICON_FOLDEROPEN = 1;
        public const int ICON_TABLE = 2;
        public const int ICON_HIERARCHY = 3;
        public const int ICON_COLUMN = 4;
        public const int ICON_CALCULATOR = 5;
        public const int ICON_KPI = 6;
        public const int ICON_MEASURE = 7;
        public const int ICON_SIGMA = 8;
        public const int ICON_CUBE = 9;
        public const int ICON_LINK = 10;
        public const int ICON_LEVEL = 11;
        public const int ICON_CALCCOLUMN = 12;
        public const int ICON_LEVEL1 = 13;
        public const int ICON_LEVEL2 = 14;
        public const int ICON_LEVEL3 = 15;
        public const int ICON_LEVEL4 = 16;
        public const int ICON_LEVEL5 = 17;
        public const int ICON_LEVEL6 = 18;
        public const int ICON_LEVEL7 = 19;
        public const int ICON_LEVEL8 = 20;
        public const int ICON_LEVEL9 = 21;
        public const int ICON_LEVEL10 = 22;
        public const int ICON_LEVEL11 = 23;
        public const int ICON_LEVEL12 = 24;
        public const int ICON_WARNING = 25;
        public const int ICON_QUESTION = 26;
        public const int ICON_METHOD = 27;
        public const int ICON_PROPERTY = 28;
        public const int ICON_EXMETHOD = 29;
        public const int ICON_ENUM = 30;
        public const int ICON_CALCTABLE = 31;
    }

}