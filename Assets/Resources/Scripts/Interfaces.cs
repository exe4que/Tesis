using UnityEngine;
public interface Idamageable
{
    void Piew(int _bulletType);
}

public interface IDestroyable
{
    void OnDeath();
}

public interface IFireable
{
    void OnShoot(LayerMask _validTarget, bool isBot);
}

public interface IBullet
{
    void SetValidTarget(LayerMask _validTarget);
}

public interface IAnimable
{
    void SetBool(string _param, bool _value);
    void SetFloat(string _param, float _value);
    void SetInt(string _param, int _value);
}

public interface IAnimable2
{
    void SetBool(string _param, bool _value);
    void SetFloat(string _param, float _value);
    void SetInt(string _param, int _value);
}