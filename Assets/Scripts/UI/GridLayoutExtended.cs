using UnityEngine;
using UnityEngine.UI;

public class GridLayoutExtended : LayoutGroup
{
    public enum FillType
    {
        Uniform,
        Width,
        Height,
        FixedColumns,
        FixedRows
    }

    public int Rows = 1;
    public int Columns = 1;

    [SerializeField] protected Vector2 _spacing = Vector2.zero;

    [SerializeField] protected FillType _fillType = FillType.Uniform;
    [SerializeField] protected Vector2 _sizeModifier = Vector2.one;

    public Vector2 CellSize = Vector2.zero;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (_fillType == FillType.Width || _fillType == FillType.Height || _fillType == FillType.Uniform)
        {
            float sqr = Mathf.Sqrt(rectChildren.Count);

            Rows = Mathf.CeilToInt(sqr);
            Columns = Mathf.CeilToInt(sqr);
        }

        if (_fillType == FillType.Width || _fillType == FillType.FixedColumns)
            Rows = Mathf.CeilToInt(transform.childCount / (float)Columns);

        if (_fillType == FillType.Height || _fillType == FillType.FixedRows)
            Columns = Mathf.CeilToInt(transform.childCount / (float)Rows);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = ((parentWidth - (_spacing.x * (Columns - 1))) / (float)Columns) - (padding.left / (float)Columns) - (padding.right / (float)Columns);
        float cellHeight = ((parentHeight - (_spacing.y * (Rows - 1))) / (float)Rows) - (padding.top / (float)Rows) - (padding.bottom / (float)Rows);

        float modifiedCellWidth = cellWidth * _sizeModifier.x;
        float modifiedCellHeight = cellHeight * _sizeModifier.y;

        CellSize.x = cellWidth;
        CellSize.y = cellHeight;

        int rowCount = 0;
        int columnCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / Columns;
            columnCount = i % Columns;

            RectTransform rectChild = rectChildren[i];

            float xPos = (CellSize.x * columnCount) + (_spacing.x * columnCount) + (padding.left);
            float yPos = (CellSize.y * rowCount) + (_spacing.y * rowCount) + (padding.top);

            xPos += (CellSize.x - (CellSize.x * _sizeModifier.x)) / 2f;
            yPos += (CellSize.y - (CellSize.y * _sizeModifier.y)) / 2f;

            SetChildAlongAxis(rectChild, 0, xPos, modifiedCellWidth);
            SetChildAlongAxis(rectChild, 1, yPos, modifiedCellHeight);
        }
    }

    public override void CalculateLayoutInputVertical()
    {    }

    public override void SetLayoutHorizontal()
    {    }

    public override void SetLayoutVertical()
    {    }

}
