
using System;

// IEquatable��Equals���\�b�h���������邱�Ƃ�ۏႷ�邽�߂̃C���^�[�t�F�[�X
public struct GridPosition : IEquatable<GridPosition>
{
  public int x;
  public int z;

  public GridPosition(int x, int z)
  {
    this.x = x;
    this.z = z;
  }

  public override bool Equals(object obj)
  {
    return obj is GridPosition position &&
           x == position.x &&
           z == position.z;
  }

  public bool Equals(GridPosition other)
  {
    return this == other;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(x, z);
  }

  // Debug.Log���g���Ƃ��̎��X�ɉ������i����͌Ăяo����̃N���X�j�N���X��ToString���Ăяo�����
  // ���̏ꍇ�AToString()�����Ȃ��ƁADebug.Log��x, y�̒l�����邱�Ƃ��ł��Ȃ�
  public override string ToString()
  {
    return $"x: {x}; z:{z}";
  }
  // ���Z�q�̃I�[�o���[�h�����Ă���B���̏ꍇ�A�O���b�h�|�W�V�����������ɂ����ꍇ�A
  // �I�[�o�[���[�h�����֐�(�ʏ��==�ł͂Ȃ�)���Ă΂��
  public static bool operator ==(GridPosition a, GridPosition b)
  {
    return a.x == b.x && a.z == b.z;
  }
  public static bool operator !=(GridPosition a, GridPosition b)
  {
    return !(a==b);
  }
}