class DoubleNode
{
  public Position left;
  public Position right;
  public DoubleNode(Position left, Position right)
  {
    this.left = left;
    this.right = right;
  }
  public bool Contains(Position position)
  {
    return left.X <= position.X && position.X <= right.X &&
           left.Y <= position.Y && position.Y <= right.Y;
  }
}