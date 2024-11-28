using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IFixedUpdatable
{
    // 이 인터페이스 사용시 무조건 사용해야함을 명시
    void OnFixedUpdate();       
}
