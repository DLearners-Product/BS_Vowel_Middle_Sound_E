using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleandMusic : MonoBehaviour
{
 
    public ParticleSystem[] Particle;
    public int particle_no;
    public Button[] BUTA_buttons;



    //public void Playparticle(int count)
    //{
    //    particle_no = count;
    //    for(int i=0;i<5;i++)
    //    {
    //        Debug.Log("par_off");
    //        Particle[i].Stop();
    //    }
    //    Debug.Log("par_on");
    //    Particle[particle_no].Play();
    //}


   // private void Awake()
   // {
   //     for (int i = 0; i < Particle.Length; i++)
   //     {
   //         //Particle[i]
   //         Particle[i].SetActive(false);
   //     }
   // }

   // public void particleselect(int level)
   // {
   //     levelno = level;
   //     for (int i = 0; i < Particle.Length; i++)
   //     {
   //         Particle[i].SetActive(false);
   //         BUTA_buttons[i].enabled = false;
   //     }
   //     Particle[levelno].SetActive(true);
   //     Invoke("THI_offparticle", 2f);
        
   // }
   //public void THI_offparticle()
   // {
   //     Particle[levelno].SetActive(false);
   //     for (int i = 0; i < Particle.Length; i++)
   //     {
           
   //         BUTA_buttons[i].enabled = true;
   //     }
   // }
}
