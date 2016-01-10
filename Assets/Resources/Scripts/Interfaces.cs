public interface Idamageable
{
    void Piew(int _bulletType);
}

public interface IDestroyable
{
    void OnDeath();
}

public interface IAnimable
{
    void SetBool(bool _value);
    void SetFloat(float _value);
    void SetInt(int _value);
}