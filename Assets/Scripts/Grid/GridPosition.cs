
public struct GridPosition
{
  public int x;
  public int z;

  public GridPosition(int x, int z)
  {
    this.x = x;
    this.z = z;
  }

  // Debug.Log���g���Ƃ��̎��X�ɉ������i����͌Ăяo����̃N���X�j�N���X��ToString���Ăяo�����
  // ���̏ꍇ�AToString()�����Ȃ��ƁADebug.Log��x, y�̒l�����邱�Ƃ��ł��Ȃ�
  public override string ToString()
  {
    return $"x: {x}; z:{z}";
  }

  //public string MatrixPosition()
  //{
  //  return $"x: {x}; z:{z}";
  //}
}