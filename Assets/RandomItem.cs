using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player")
        {
            // 체력 회복
            if (Player.Ins.curHp < Player.Ins.maxHp)
            {
                float recovery = Random.Range(1, Player.Ins.maxHp - 1);
                Player.Ins.curHp += recovery;
            }

            // 데미지
            if (Player.Ins.curHp > 1)
            {
                float damage = Random.Range(1, Player.Ins.curHp - 1);
                Player.Ins.curHp -= damage;
            }
            
            // 체력 증가
            Player.Ins.maxHp++;
            // 체력 감소
            Player.Ins.maxHp--;
            // 공속 증가
            Player.Ins.attackSpeed++;
            // 공속 다운
            Player.Ins.attackSpeed--;
            // 총알 장전
            GunController.Ins.curAmmo++;
            // 총알 제거
            GunController.Ins.curAmmo--;
            // 탄창 증가
            GunController.Ins.maxAmmo++;
            // 탄창 감소
            GunController.Ins.maxAmmo++;
            // 이속 증가
            Player.Ins.MoveSpeed++;
            // 이속 다운
            Player.Ins.MoveSpeed--;
            // 데미지 증가
            GunController.Ins.damage++;
            // 데미지 감소
            GunController.Ins.damage--;

            Destroy(gameObject);
        }

    }
}
