using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class EnablerCmtsDB
{
    public string welcome;
    public string short_vowels;
    public string review_a;
    public string intro_to_vowels_e;
    public string e_intro_red;
    public string e_intro_bed;
    public string vowel_e_to_make_words1;
    public string vowel_e_to_words2;
    public string e_middle_sound_evaluation;
    public string e_middle_sound_unscramble;
    public string e_middle_sound_visual;
    public string e_middle_sound_auditory;
    public string e_sound_poem;
    public string goodbye;

    public EnablerCmtsDB()
    {
        welcome = Main_Blended.OBJ_main_blended.enablerComments[0];
        short_vowels = Main_Blended.OBJ_main_blended.enablerComments[1];
        review_a = Main_Blended.OBJ_main_blended.enablerComments[2];
        intro_to_vowels_e = Main_Blended.OBJ_main_blended.enablerComments[3];
        e_intro_red = Main_Blended.OBJ_main_blended.enablerComments[4];
        e_intro_bed = Main_Blended.OBJ_main_blended.enablerComments[5];
        vowel_e_to_make_words1 = Main_Blended.OBJ_main_blended.enablerComments[6];
        vowel_e_to_words2 = Main_Blended.OBJ_main_blended.enablerComments[7];
        e_middle_sound_evaluation = Main_Blended.OBJ_main_blended.enablerComments[8];
        e_middle_sound_unscramble = Main_Blended.OBJ_main_blended.enablerComments[9];
        e_middle_sound_visual = Main_Blended.OBJ_main_blended.enablerComments[10];
        e_middle_sound_auditory = Main_Blended.OBJ_main_blended.enablerComments[11];
        e_sound_poem = Main_Blended.OBJ_main_blended.enablerComments[12];
        goodbye = Main_Blended.OBJ_main_blended.enablerComments[13];
    }
}