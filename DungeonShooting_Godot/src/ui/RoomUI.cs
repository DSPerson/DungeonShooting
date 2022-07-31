using System;
using Godot;

/// <summary>
/// 房间中的ui
/// </summary>
public class RoomUI : Control
{

    public static RoomUI Current { get; private set; }

    /// <summary>
    /// 当前血量
    /// </summary>
    public int Hp { get; private set; }
    /// <summary>
    /// 最大血量
    /// </summary>
    public int MaxHp { get; private set; }
    /// <summary>
    /// 当前护盾值
    /// </summary>
    public int Shield { get; private set; }
    /// <summary>
    /// 最大护盾值
    /// </summary>
    public int MaxShield { get; private set; }


    private NinePatchRect hpSlot;
    private NinePatchRect shieldSlot;
    private TextureRect hpBar;
    private TextureRect shieldBar;
    private Label bulletText;
    private TextureRect gunSprite;

    public override void _EnterTree()
    {
        Current = this;
    }

    public override void _Ready()
    {
        hpSlot = GetNode<NinePatchRect>("Control/HealthBar/HpSlot");
        shieldSlot = GetNode<NinePatchRect>("Control/HealthBar/ShieldSlot");
        hpBar = GetNode<TextureRect>("Control/HealthBar/HpSlot/HpBar");
        shieldBar = GetNode<TextureRect>("Control/HealthBar/ShieldSlot/ShieldBar");

        bulletText = GetNode<Label>("Control/GunBar/BulletText");
        gunSprite = GetNode<TextureRect>("Control/GunBar/GunSprite");
    }

    /// <summary>
    /// 设置最大血量
    /// </summary>
    public void SetMaxHp(int maxHp)
    {
        MaxHp = Mathf.Max(maxHp, 0);
        hpSlot.RectSize = new Vector2(maxHp + 3, hpSlot.RectSize.y);
        if (Hp > maxHp)
        {
            SetHp(maxHp);
        }
    }

    /// <summary>
    /// 设置最大护盾值
    /// </summary>
    public void SetMaxShield(int maxShield)
    {
        MaxShield = Mathf.Max(maxShield, 0); ;
        shieldSlot.RectSize = new Vector2(maxShield + 2, shieldSlot.RectSize.y);
        if (Shield > MaxShield)
        {
            SetShield(maxShield);
        }
    }

    /// <summary>
    /// 设置当前血量
    /// </summary>
    public void SetHp(int hp)
    {
        Hp = Mathf.Clamp(hp, 0, MaxHp);
        hpBar.RectSize = new Vector2(hp, hpBar.RectSize.y);
    }

    /// <summary>
    /// 设置护盾值
    /// </summary>
    public void SetShield(int shield)
    {
        Shield = Mathf.Clamp(shield, 0, MaxShield);
        shieldBar.RectSize = new Vector2(shield, shieldBar.RectSize.y);
    }

    /// <summary>
    /// 玩家受到伤害
    /// </summary>
    public void Hit(int num)
    {

    }

    /// <summary>
    /// 设置显示在 ui 上的枪的纹理
    /// </summary>
    /// <param name="gun">纹理</param>
    public void SetGunTexture(Texture gun)
    {
        if (gun != null)
        {
            gunSprite.Texture = gun;
            gunSprite.Visible = true;
            bulletText.Visible = true;
        }
        else
        {
            gunSprite.Visible = false;
            bulletText.Visible = false;
        }
    }

    /// <summary>
    /// 设置弹药数据
    /// </summary>
    /// <param name="curr">当前弹夹弹药量</param>
    /// <param name="total">剩余弹药总数</param>
    public void SetAmmunition(int curr, int total)
    {
        bulletText.Text = curr + " / " + total;
    }
}