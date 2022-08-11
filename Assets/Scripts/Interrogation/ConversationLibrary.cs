using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLibrary : MonoBehaviour
{
    [SerializeField] public string CurrentKCM = "...Hello?";
    [SerializeField] public string CurrentKPM = "-";
    [SerializeField] public string CurrentYCM = "-";
    [SerializeField] public string CurrentYPM = "-";
    [SerializeField] public string CurrentO1 = "Friendly";
    [SerializeField] public string CurrentO2 = "Hostile";
    public int cycle = 0;
    public bool Hostile = false;
    public bool BranchA = false;
    public bool BranchAA = false;
    public bool BranchBA = false;

    public string[] FriendlyPlayerChatVariants = new string[]
    {
        "-",
        "Hello, I am the lead interrogator, I'm here to talk and resolve this situation peacefully, are you willing to talk?", // 0 1A
        "Understood, the car will arrive in an hour. However this is only if you agree to release the hostage when you get into the vehicle. Can we agree to that?", // 1 2A
        "Unfortunately, we can't agree to that. Release the hostage, let's end this peacefully and noone will get hurt. We will note the judge you were co-operative, and your sentence will be reduced.", // 2 2B
        "Okay, the car is on the way. We agree to your terms.", // 3 3AA END 2
        "We can't agree to that. We need to make sure the hostage is safe and sound, and then we can let you go. Unfortunately, there is no other way.", // 4 3AB
        "Okay, we agree. The car is on the way, and will arrive shortly.", // 5 4ABA END 2
        "We can't do that. We need the hostage safe and sound, now.", // 6 4ABB END 3
        "The snipers will be disarmed and I will personally approach you if you let the hostage go and leave all your weapons away from yourself. Can we agree to that?", // 7 3BA END 1
        "You're dead either way. So give up right now, before we breach the place and take you down." // 8 3BB END 3
    };

    public string[] FriendlyKidnapperChatVariants = new string[] 
    {   
        "...Hello?",
        "Yes, I'm willing to talk. I want a car with no plate numbers, I will release the hostage in a safe area after I make sure I am not being followed. This is my request.", // 0 Start
        "I... I don't know. How can i be sure you are not lying about letting me go? The hostage is my only leaverage. You know what, screw the money. Just get me the car, and I promise to leave the hostage safe and sound.", // 1 1A
        "I... I can't do that. How do I know I'll won't get shot the moment I leave?",  // 2 1B
        "Rodger that", // 3 2AA END 2
        "No. Screw this nice act... Get me the car or I'm killing the hostage. This is my last offer.", // 4 2AB
        "Okay, I'm waiting", // 5 3ABA END 2
        "You asked for it. Try and get me if you can. *gunshot sounds*", // 6 3ABB END 3
        "I-I think so. Okay the hostage is released. I'm going out!", // 7 2BA END 1
        "Oh, just as I thought. *Gunshot sounds*. The hostage is dead now. Take me if you can, but I'm taking you with me." // 8 2BB END 3
    };

    public string[] HostilePlayerChatVariants = new string[]
    {
        "-",
        "This is the FBI, you are surrounded. Get out with your hands closed and you will not be shot.", // 0
        "We agree to your terms. The car is on the way.", // 1 END 2
        "We repeat, get out with your hands up, or we will take you down. This is your only option." // 2 END 3
    };

    public string[] HostileKidnapperChatVariants = new string[] 
    {
        "...Hello?",
        "You do not make the rules. I want a car and 100k dollars in cash, as soon as I make my escape I will leave the hostage in a safe area. Otherwise, I will shoot the hostage.",  // 0
        "I'm waiting",  // 1 END 2
        "Got it. Good luck taking me alive! *gunshot sounds*"  // 2 END 3
    };

    public string[] ButtonTexts = new string[]
    {
        "END", // 0
        "Agree",  // 1
        "Disagree",  // 2
        "Reassure",  // 3
        "Threaten" // 4
    };

}
