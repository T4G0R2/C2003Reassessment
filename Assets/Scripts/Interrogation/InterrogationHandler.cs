using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterrogationHandler : MonoBehaviour
{
    private GameObject player;
    private ConversationLibrary CV;
    private int ChatOption;
    private GameObject KCMGO;
    private GameObject KPMGO;
    private GameObject YCMGO;
    private GameObject YPMGO;
    private GameObject O1GO;
    private GameObject O2GO;
    private Text KCMGOT;
    private Text KPMGOT;
    private Text YCMGOT;
    private Text YPMGOT;
    private Text O1GOT;
    private Text O2GOT;
    private GameEnd GE;
    // Start is called before the first frame update
    void Start()
    {
        KCMGO = GameObject.FindGameObjectWithTag("KCM");
        KPMGO = GameObject.FindGameObjectWithTag("KPM");
        YCMGO = GameObject.FindGameObjectWithTag("YCM");
        YPMGO = GameObject.FindGameObjectWithTag("YPM");
        O1GO = GameObject.FindGameObjectWithTag("O1");
        O2GO = GameObject.FindGameObjectWithTag("O2");
        player = GameObject.FindGameObjectWithTag("Player");

        KCMGOT = KCMGO.GetComponent<Text>();
        KPMGOT = KPMGO.GetComponent<Text>();
        YCMGOT = YCMGO.GetComponent<Text>();
        YPMGOT = YPMGO.GetComponent<Text>();
        O1GOT = O1GO.GetComponent<Text>();
        O2GOT = O2GO.GetComponent<Text>();
        CV = player.GetComponent<ConversationLibrary>();
        GE = player.GetComponent<GameEnd>();
    }

    public void Option1()
    {
        TriggerMessage(1);
    }

    public void Option2()
    {
        TriggerMessage(2);
    }

    private void TriggerMessage(int option)
    {
        if (CV.cycle == 0)
        {
            if(option == 1) // 1A
            {
                CV.Hostile = false;
                TriggerChanges(option, CV.cycle, CV.Hostile);
            }
            else if (option == 2) // 1B
            {
                CV.Hostile = true;
                TriggerChanges(option, CV.cycle, CV.Hostile);
            }
        }
        else if (CV.cycle == 1 && CV.Hostile == false)
        {
            if (option == 1) // 2A
            {
                CV.BranchA = true;
                TriggerChanges(option, CV.cycle, CV.Hostile);
            }
            else if (option == 2) // 2B
            {
                CV.BranchA = false;
                TriggerChanges(option, CV.cycle, CV.Hostile);
            }
        }
        else if (CV.cycle == 2 && CV.Hostile == false && CV.BranchA == true)
        {
            if (option == 1) // 3AA END
            {
                CV.BranchAA = true;
                TriggerChanges(option, CV.cycle, CV.Hostile);
                TriggerEnding(2);
            }
            else if (option == 2) // 3AB
            {
                CV.BranchAA = false;
                TriggerChanges(option, CV.cycle, CV.Hostile);
            }
        }
        else if (CV.cycle == 2 && CV.Hostile == false && CV.BranchA == false)
        {
            if (option == 1) // 3BA END 
            {
                CV.BranchBA = true;
                TriggerChanges(option, CV.cycle, CV.Hostile);
                TriggerEnding(1);
            }
            else if (option == 2) // 3BB END
            {
                CV.BranchBA = false;
                TriggerChanges(option, CV.cycle, CV.Hostile);
                TriggerEnding(3);
            }
        }
        else if (CV.cycle == 3 && CV.Hostile == false && CV.BranchA == true && CV.BranchAA == false)
        {
            if (option == 1) // 3ABA END
            {
                TriggerChanges(option, CV.cycle, CV.Hostile);
                TriggerEnding(2);
            }
            else if (option == 2) // 3ABB END
            {
                TriggerChanges(option, CV.cycle, CV.Hostile);
                TriggerEnding(3);
            }
        }
        else if (CV.cycle == 1 && CV.Hostile == true)
        {
            if (option == 1) // 2A
            {
                TriggerChanges(option, CV.cycle, CV.Hostile);
                TriggerEnding(2);
            }
            else if (option == 2) // 2B
            {
                TriggerChanges(option, CV.cycle, CV.Hostile);
                TriggerEnding(3);
            }
        }

        CV.cycle = CV.cycle + 1;
    }

    private void TriggerChanges(int option, int cycle, bool Hostile)
    {
        if(Hostile == false && cycle == 0) // 1A
        {
            KCMGOT.text = CV.FriendlyKidnapperChatVariants[1];
            KPMGOT.text = CV.FriendlyKidnapperChatVariants[0];
            YCMGOT.text = CV.FriendlyPlayerChatVariants[1];
            YPMGOT.text = CV.FriendlyPlayerChatVariants[0];
            O1GOT.text = CV.ButtonTexts[1];
            O2GOT.text = CV.ButtonTexts[2];
        }

        if (Hostile == false && cycle == 1 && CV.BranchA == true) // 2A
        {
            KCMGOT.text = CV.FriendlyKidnapperChatVariants[2];
            KPMGOT.text = CV.FriendlyKidnapperChatVariants[1];
            YCMGOT.text = CV.FriendlyPlayerChatVariants[2];
            YPMGOT.text = CV.FriendlyPlayerChatVariants[1];
            O1GOT.text = CV.ButtonTexts[1];
            O2GOT.text = CV.ButtonTexts[2];
        }

        if (Hostile == false && cycle == 1 && CV.BranchA == false) // 2B
        {
            KCMGOT.text = CV.FriendlyKidnapperChatVariants[3];
            KPMGOT.text = CV.FriendlyKidnapperChatVariants[1];
            YCMGOT.text = CV.FriendlyPlayerChatVariants[3];
            YPMGOT.text = CV.FriendlyPlayerChatVariants[1];
            O1GOT.text = CV.ButtonTexts[3];
            O2GOT.text = CV.ButtonTexts[4];
        }

        if (Hostile == false && cycle == 2 && CV.BranchAA == true && CV.BranchA == true) // 3AA
        {
            KCMGOT.text = CV.FriendlyKidnapperChatVariants[4];
            KPMGOT.text = CV.FriendlyKidnapperChatVariants[2];
            YCMGOT.text = CV.FriendlyPlayerChatVariants[4];
            YPMGOT.text = CV.FriendlyPlayerChatVariants[2];
            O1GOT.text = CV.ButtonTexts[0];
            O2GOT.text = CV.ButtonTexts[0];
        }

        if (Hostile == false && cycle == 2 && CV.BranchBA == true && CV.BranchA == false) // 3BA
        {
            KCMGOT.text = CV.FriendlyKidnapperChatVariants[8];
            KPMGOT.text = CV.FriendlyKidnapperChatVariants[3];
            YCMGOT.text = CV.FriendlyPlayerChatVariants[8];
            YPMGOT.text = CV.FriendlyPlayerChatVariants[3];
            O1GOT.text = CV.ButtonTexts[0];
            O2GOT.text = CV.ButtonTexts[0];
        }

        if (Hostile == false && cycle == 2 && CV.BranchBA == false && CV.BranchA == false) // 3BB
        {
            KCMGOT.text = CV.FriendlyKidnapperChatVariants[9];
            KPMGOT.text = CV.FriendlyKidnapperChatVariants[3];
            YCMGOT.text = CV.FriendlyPlayerChatVariants[9];
            YPMGOT.text = CV.FriendlyPlayerChatVariants[3];
            O1GOT.text = CV.ButtonTexts[0];
            O2GOT.text = CV.ButtonTexts[0];
        }

        if (Hostile == false && cycle == 2 && CV.BranchAA == false && CV.BranchA == true) // 3AB
        {
            KCMGOT.text = CV.FriendlyKidnapperChatVariants[5];
            KPMGOT.text = CV.FriendlyKidnapperChatVariants[2];
            YCMGOT.text = CV.FriendlyPlayerChatVariants[5];
            YPMGOT.text = CV.FriendlyPlayerChatVariants[2];
            O1GOT.text = CV.ButtonTexts[1];
            O2GOT.text = CV.ButtonTexts[2];
        }

        if (Hostile == false && cycle == 3 && CV.BranchAA == false && CV.BranchA == true && option == 1) // 3ABA
        {
            KCMGOT.text = CV.FriendlyKidnapperChatVariants[6];
            KPMGOT.text = CV.FriendlyKidnapperChatVariants[5];
            YCMGOT.text = CV.FriendlyPlayerChatVariants[6];
            YPMGOT.text = CV.FriendlyPlayerChatVariants[5];
            O1GOT.text = CV.ButtonTexts[0];
            O2GOT.text = CV.ButtonTexts[0];
        }

        if (Hostile == false && cycle == 3 && CV.BranchAA == false && CV.BranchA == true && option == 2) // 3ABB
        {
            KCMGOT.text = CV.FriendlyKidnapperChatVariants[7];
            KPMGOT.text = CV.FriendlyKidnapperChatVariants[5];
            YCMGOT.text = CV.FriendlyPlayerChatVariants[7];
            YPMGOT.text = CV.FriendlyPlayerChatVariants[5];
            O1GOT.text = CV.ButtonTexts[0];
            O2GOT.text = CV.ButtonTexts[0];
        }
        if (Hostile == true && cycle == 1 && option == 1) // EA
        {
            KCMGOT.text = CV.HostileKidnapperChatVariants[2];
            KPMGOT.text = CV.HostileKidnapperChatVariants[1];
            YCMGOT.text = CV.HostilePlayerChatVariants[2];
            YPMGOT.text = CV.HostilePlayerChatVariants[1];
            O1GOT.text = CV.ButtonTexts[0];
            O2GOT.text = CV.ButtonTexts[0];
        }
        if (Hostile == true && cycle == 1 &&  option == 2) // EB
        {
            KCMGOT.text = CV.HostileKidnapperChatVariants[3];
            KPMGOT.text = CV.HostileKidnapperChatVariants[1];
            YCMGOT.text = CV.HostilePlayerChatVariants[3];
            YPMGOT.text = CV.HostilePlayerChatVariants[1];
            O1GOT.text = CV.ButtonTexts[0];
            O2GOT.text = CV.ButtonTexts[0];
        }
        if (Hostile == true && cycle == 0) // E
        {
            KCMGOT.text = CV.HostileKidnapperChatVariants[1];
            KPMGOT.text = CV.HostileKidnapperChatVariants[0];
            YCMGOT.text = CV.HostilePlayerChatVariants[1];
            YPMGOT.text = CV.HostilePlayerChatVariants[0];
            O1GOT.text = CV.ButtonTexts[1];
            O2GOT.text = CV.ButtonTexts[4];
        }
    }

    private void TriggerEnding(int EndingID)
    {
        GE.EndGame(EndingID);
    }
}
