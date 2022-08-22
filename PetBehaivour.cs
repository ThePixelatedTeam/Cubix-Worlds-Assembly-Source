// Decompiled with JetBrains decompiler
// Type: PetBehaivour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 317A70D6-49A7-4DF1-8352-E161673EC7DD
// Assembly location: C:\Users\ASUS\Downloads\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class PetBehaivour : MonoBehaviour
{
  public GameObject player;
  public TextMeshProUGUI title;
  public Vector2 size;
  public float randomAimCooldown;
  public float randomMovementSpeed;
  private int direction;
  private Vector2 offset;
  private Vector2 currentRandomMovementDestination;
  private Vector2 previousPosition;
  private Vector2 previousScale;

  private IEnumerator SelectRandomPoint()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    PetBehaivour petBehaivour = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      petBehaivour.StartCoroutine(petBehaivour.SelectRandomPoint());
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    petBehaivour.offset = new Vector2(Random.Range(0.0f, petBehaivour.size.x), Random.Range(0.0f, petBehaivour.size.y));
    petBehaivour.direction = 1 - petBehaivour.direction;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) new WaitForSeconds(petBehaivour.randomAimCooldown);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  private void Start() => this.StartCoroutine(this.SelectRandomPoint());

  private void Update()
  {
  }

  private void FixedUpdate()
  {
    if (!Object.op_Inequality((Object) this.player, (Object) null))
      return;
    Vector2 vector2;
    if (this.direction == 0)
    {
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(this.player.transform.position.x + 0.7f + this.offset.x, this.player.transform.position.y + this.offset.y);
    }
    else
    {
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(this.player.transform.position.x - 0.7f - this.offset.x, this.player.transform.position.y + this.offset.y);
    }
    ((Component) this).transform.position = Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(((Component) this).transform.position), vector2, Time.deltaTime * this.randomMovementSpeed));
    if ((double) ((Component) this).transform.position.x > (double) this.previousPosition.x)
      ((Component) this).transform.localScale = Vector2.op_Implicit(new Vector2(Mathf.Abs(((Component) this).transform.localScale.x), ((Component) this).transform.localScale.y));
    else
      ((Component) this).transform.localScale = Vector2.op_Implicit(new Vector2(-Mathf.Abs(((Component) this).transform.localScale.x), ((Component) this).transform.localScale.y));
    if (Object.op_Inequality((Object) this.title, (Object) null))
      ((TMP_Text) this.title).transform.position = Vector2.op_Implicit(new Vector2(((Component) this).transform.position.x, ((Component) this).transform.position.y + ((Component) this).transform.localScale.y / 2.5f));
    this.previousPosition = Vector2.op_Implicit(((Component) this).transform.position);
    this.previousScale = Vector2.op_Implicit(((Component) this).transform.localScale);
  }
}
