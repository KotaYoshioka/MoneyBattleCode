using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoData : MonoBehaviour
{
    //�d�͑���
    //�_���[�W
    public readonly static int CONT_DAMAGE = 20;
    //����𐁂���΂���
    public readonly static int CONT_FORCE = 1000;
    //���肪�����Ȃ�����
    public readonly static float CONT_STUCK_SECS = 0.8f;
    
    //���d�͌Œ�
    //����ւ̃X�^������
    public readonly static int ZERO_STAN_SECS = 3;

    //���V
    //���d�͎���(���̋Z���́A�A�j���[�V�����ˑ��Ȃ̂ŁA�ύX�̏ꍇ�A�A�j���[�V������)
    public readonly static int FUYU_LONG_SECS = 4;

    //�����e
    //�_���[�W
    public readonly static int DAN_DAMAGE = 140;
    //�e�̔��ˈЗ�
    public readonly static int DAN_SPEED = 1000;
    //����
    public readonly static int DAN_RECOIL = 600;
    //�󒆐Î~����
    public readonly static float DAN_STOP_AIR_SECS = 1.5f;
}
