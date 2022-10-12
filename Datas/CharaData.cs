using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaData
{
    //<------�L�����̏��------>
    //0:�L������(string)
    //1:�p�L������(string)
    //2:�ő�̗�(int)
    //3:�ړ����x(float)
    //4:�W�����v��(int)
    //5:�W�����v��(int)
    //6:��������(int)
    //7:�f�U�C��(string)
    private static object[,] charas = {
        {"�R�E�u�W��",   "Koubuzin",    600,    5.6f,   550,    1, 12,  "�M�j"},
        {"���m",         "Mono",        300,    4.5f,   650,    2,  6,  "���^"},
        {"�t���[�g",     "Freat",       250,    4.7f,   600,    1,  5,  "�^�R�X"},
        {"�A�N��",       "Akumu",       500,    3.6f,   400,    2, 18,  "���^"},
        {"�u�����`�F",   "Branche",     400,    4.2f,   350,    2, 10,  "�M�j"},
        {"�N���K�̃A�C�c","CrazyA",     300,    4.7f,   600,    3, 14,  "�f���N�V�E�����E�f���N�V"},
        {"�_���}��",     "Daruman",     350,    3.8f,   200,    4,  8,  "�݁[����"},
        {"���ǂ낭�]�c", "OdorokuMotita",350,   4.3f,   300,    2,  8,  "�Ƃ�̂�"},
        {"�j�R",         "Nico",        350,    4.5f,   550,    2,  7,  "���^"},
        {"�J�~��",       "Kamiya",      400,    4.9f,   700,    1,  6,  "�J�~��"},
        {"�j���U�[�h",   "Ninzard",     150,    8.2f,   500,    2,  8,  "�M�j"}
    };

    //<------�Z------->
    //0:�Z��(string)
    //1:�N�[���_�E��(int)
    //2:�Z�̐���(string)
    private static object[,,] waza =
    {
        //�R�E�u�W��
        {
            {"�d��",17,"�������Ă���" + KoubuzinData.KOKA_SPECIAL_SECS + "�b�ԁA���G��ԂɂȂ�A���̌�A" + KoubuzinData.KOKA_DAMAGE_REDUCE_SECS + "�b�ԁA" + KoubuzinData.KOKA_DAMAGE_REDUCE_POWER + "�_���[�W�y������������B�܂��A���̋Z�̌��ʒ��A�����̈ړ����x�͔�������B"},
            {"�Ζ�",12,"�����Ă�������Ɍ������āA�n�ʂ�����1��яo������B����" + KoubuzinData.MON_SECOND_DELAY_SECS + "�b��A����1�₪��яo��B�ڐG�����G�v���C���[�𐁂���΂��A1�ڂ̊��" + KoubuzinData.MON_FIRST_DAMAGE + "�_���[�W�A2�ڂ̊��" + KoubuzinData.MON_SECOND_DAMAGE + "�_���[�W�^����B"},
            {"��Η���",6,"�����Ă�������̓V�䂩���𗎂��B��ɂԂ������G�v���C���[��" + KoubuzinData.OTOSHI_DAMAGE + "�_���[�W��^����B"},
            {"����",35,"�����Ă�������ɑ傫�Ȑ΂̕ǂ�" + KoubuzinData.KYO_LIVE_SECS + "�b��������B"}
        },
        //���m
        {
            {"�d�͑���",14,""},
            {"���d�͌Œ�",20,""},
            {"���V",12,""},
            {"�����e",13,""}
        },
        //�t���[�g
        {
            {"��������",16,""},
            {"�傩����",22,""},
            {"��������",3,""},
            {"��������",4,""}
        },
        //�A�N��
        {
            {"����",15,""},
            {"�����e",7,""},
            {"�g���E�}",23,""},
            {"�Ď�",0,""}
        },
        //�u�����`�F
        {
            {"�A��",17,""},
            {"�؂̗t",5,""},
            {"�����ς��",15,""},
            {"�}�܂�",3,""}
        },
        //�N���K�̃A�C�c
        {
            {"����̒���",18,""},
            {"���퉮�̂�����",15,""},
            {"�|���J���V���b�g",2,""},
            {"�_�C�i�~�b�N����",25,""}
        },
        //�_���}��
        {
            {"�̓�����",10,""},
            {"���]",8,""},
            {"����܂��񂪓]��",0,""},
            {"�N���オ��",0,""}
        },
        //���ǂ낭�]�c
        {
            {"����͂Ȃ�����I�H",7,""},
            {"�ɂĂ��Ȃ����I�I",5,""},
            {"�ق���āI",16,""},
            {"�S�R���˂��ȁI",17,""}
        },
        //�j�R
        {
            {"�t���E",4,""},
            {"�C�Â��ʂ�����",9,""},
            {"�T����",8,""},
            {"��",135,""}
        },
        //�J�~��
        {
            {"�n�쒀",6,""},
            {"�d�͔n",16,""},
            {"�n��",10,""},
            {"�n��",4,""}
        },
        //�j���U�[�h
        {
            {"���Ȃ�",1,""},
            {"�g����",10,""},
            {"�܂��т�",5,""},
            {"�ދp�p",15,""}
        }
    };

    //Getter��Setter
    public static string GetCharaName(int charaID)
    {
        return (string)charas[charaID, 0];
    }
    public static string GetCharaNameEnglish(int charaID)
    {
        return (string)charas[charaID, 1];
    }
    public static int GetCharaLength()
    {
        return charas.GetLength(0);
    }

    public static int GetHP(int charaID)
    {
        return (int)charas[charaID,2];
    }
    public static float GetSpeed(int charaID)
    {
        return (float)charas[charaID, 3];
    }
    public static int GetJump(int charaID)
    {
        return (int)charas[charaID, 4];
    }
    public static int GetJumpTime(int charaID)
    {
        return (int)charas[charaID, 5];
    }
    public static int GetRespawn(int charaID)
    {
        return (int)charas[charaID, 6];
    }
    public static string GetDesign(int charaID)
    {
        return (string)charas[charaID, 7];
    }
    public static string GetAbilityName(int charaID,int abilityID)
    {
        return (string)waza[charaID, abilityID, 0];
    }
    public static int GetCooldown(int charaID,int abilityID)
    {
        return (int)waza[charaID, abilityID,1];
    }
}
