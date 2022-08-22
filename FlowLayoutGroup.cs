// Decompiled with JetBrains decompiler
// Type: FlowLayoutGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Layout/Flow Layout Group", 153)]
public class FlowLayoutGroup : LayoutGroup
{
  protected Vector2 m_CellSize = new Vector2(100f, 100f);
  [SerializeField]
  protected Vector2 m_Spacing = Vector2.zero;
  private int cellsPerMainAxis;
  private int actualCellCountX;
  private int actualCellCountY;
  private int positionX;
  private int positionY;
  private float totalWidth;
  private float totalHeight;
  private float lastMaxHeight;

  public Vector2 cellSize
  {
    get => this.m_CellSize;
    set => this.SetProperty<Vector2>(ref this.m_CellSize, value);
  }

  public Vector2 spacing
  {
    get => this.m_Spacing;
    set => this.SetProperty<Vector2>(ref this.m_Spacing, value);
  }

  protected FlowLayoutGroup()
  {
  }

  public virtual void CalculateLayoutInputHorizontal()
  {
    base.CalculateLayoutInputHorizontal();
    this.SetLayoutInputForAxis((float) this.padding.horizontal + (float) (((double) this.cellSize.x + (double) this.spacing.x) * 1.0) - this.spacing.x, (float) this.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float) Mathf.CeilToInt(Mathf.Sqrt((float) this.rectChildren.Count)) - this.spacing.x, -1f, 0);
  }

  public virtual void CalculateLayoutInputVertical()
  {
    Rect rect = this.rectTransform.rect;
    Mathf.Max(1, Mathf.FloorToInt((float) (((double) ((Rect) ref rect).size.x - (double) this.padding.horizontal + (double) this.spacing.x + 1.0 / 1000.0) / ((double) this.cellSize.x + (double) this.spacing.x))));
    float num = (float) this.padding.vertical + (float) (((double) this.cellSize.y + (double) this.spacing.y) * 1.0) - this.spacing.y;
    this.SetLayoutInputForAxis(num, num, -1f, 1);
  }

  public virtual void SetLayoutHorizontal() => this.SetCellsAlongAxis(0);

  public virtual void SetLayoutVertical() => this.SetCellsAlongAxis(1);

  private void SetCellsAlongAxis(int axis)
  {
    Rect rect1 = this.rectTransform.rect;
    float x1 = ((Rect) ref rect1).size.x;
    Rect rect2 = this.rectTransform.rect;
    float y1 = ((Rect) ref rect2).size.y;
    int num1 = (double) this.cellSize.x + (double) this.spacing.x > 0.0 ? Mathf.Max(1, Mathf.FloorToInt((float) (((double) x1 - (double) this.padding.horizontal + (double) this.spacing.x + 1.0 / 1000.0) / ((double) this.cellSize.x + (double) this.spacing.x)))) : int.MaxValue;
    int num2 = (double) this.cellSize.y + (double) this.spacing.y > 0.0 ? Mathf.Max(1, Mathf.FloorToInt((float) (((double) y1 - (double) this.padding.vertical + (double) this.spacing.y + 1.0 / 1000.0) / ((double) this.cellSize.y + (double) this.spacing.y)))) : int.MaxValue;
    this.cellsPerMainAxis = num1;
    this.actualCellCountX = Mathf.Clamp(num1, 1, this.rectChildren.Count);
    this.actualCellCountY = Mathf.Clamp(num2, 1, Mathf.CeilToInt((float) this.rectChildren.Count / (float) this.cellsPerMainAxis));
    Vector2 vector2_1;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_1).\u002Ector((float) ((double) this.actualCellCountX * (double) this.cellSize.x + (double) (this.actualCellCountX - 1) * (double) this.spacing.x), (float) ((double) this.actualCellCountY * (double) this.cellSize.y + (double) (this.actualCellCountY - 1) * (double) this.spacing.y));
    Vector2 vector2_2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_2).\u002Ector(this.GetStartOffset(0, vector2_1.x), this.GetStartOffset(1, vector2_1.y));
    this.totalWidth = 0.0f;
    this.totalHeight = 0.0f;
    Vector2 zero = Vector2.zero;
    for (int index = 0; index < this.rectChildren.Count; ++index)
    {
      RectTransform rectChild1 = this.rectChildren[index];
      double num3 = (double) vector2_2.x + (double) this.totalWidth;
      Rect rect3 = this.rectChildren[index].rect;
      double x2 = (double) ((Rect) ref rect3).size.x;
      this.SetChildAlongAxis(rectChild1, 0, (float) num3, (float) x2);
      RectTransform rectChild2 = this.rectChildren[index];
      double num4 = (double) vector2_2.y + (double) this.totalHeight;
      rect3 = this.rectChildren[index].rect;
      double y2 = (double) ((Rect) ref rect3).size.y;
      this.SetChildAlongAxis(rectChild2, 1, (float) num4, (float) y2);
      Vector2 spacing = this.spacing;
      double totalWidth1 = (double) this.totalWidth;
      rect3 = this.rectChildren[index].rect;
      double num5 = (double) ((Rect) ref rect3).width + (double) ((Vector2) ref spacing)[0];
      this.totalWidth = (float) (totalWidth1 + num5);
      rect3 = this.rectChildren[index].rect;
      if ((double) ((Rect) ref rect3).height > (double) this.lastMaxHeight)
      {
        rect3 = this.rectChildren[index].rect;
        this.lastMaxHeight = ((Rect) ref rect3).height;
      }
      if (index < this.rectChildren.Count - 1)
      {
        double totalWidth2 = (double) this.totalWidth;
        rect3 = this.rectChildren[index + 1].rect;
        double width = (double) ((Rect) ref rect3).width;
        if (totalWidth2 + width + (double) ((Vector2) ref spacing)[0] > (double) x1)
        {
          this.totalWidth = 0.0f;
          this.totalHeight += this.lastMaxHeight + ((Vector2) ref spacing)[1];
          this.lastMaxHeight = 0.0f;
        }
      }
    }
  }

  public enum Corner
  {
    UpperLeft,
    UpperRight,
    LowerLeft,
    LowerRight,
  }

  public enum Constraint
  {
    Flexible,
    FixedColumnCount,
    FixedRowCount,
  }
}
