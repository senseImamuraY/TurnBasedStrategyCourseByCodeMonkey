
public struct GridPosition
{
  public int x;
  public int z;

  public GridPosition(int x, int z)
  {
    this.x = x;
    this.z = z;
  }

  // Debug.Logを使うとその時々に応じた（今回は呼び出し先のクラス）クラスのToStringが呼び出される
  // この場合、ToString()をしないと、Debug.Logでx, yの値を見ることができない
  public override string ToString()
  {
    return $"x: {x}; z:{z}";
  }

  //public string MatrixPosition()
  //{
  //  return $"x: {x}; z:{z}";
  //}
}