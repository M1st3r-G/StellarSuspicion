---
owner: programmer
---

```mermaid

classDiagram
        PlayerSwitchManager <--> FirstPersonController
		EntryChecker <--> Creature
		CreatureGenerator <--> Creature
		FirstPersonController <--> IInteract
		UIManager <--> DialogueController
		UIManager <--> SettingsMenu
		UIManager <--> MainMenu
		SettingsMenu <--> AudioManager
		SettingsMenu <--> FirstPersonController

        class GameManager{
            +DayInformation
            #OnNewDay()
        }
		
		class FirstPersonController{
			+HandleInput()
			+Interact()
		}
	
		class IInteract{
			<<interface>>
		}

		class PlayerSwitchManager{
			+SwitchCamera()
			+SetCameraActive(int index, bool state)
		}

		class TimeManager{
			+SetTimerActive(bool state)
			#OnDayEnd
			#OnTimeEvent
		}

        class CreatureGenerator{
	        +GetCreature()
        }
    
        class EventHandler{
	        TriggerRandomEvent()
	        TriggerEvent(int index)
        }
		
		class AudioManager{
			+PlaySoundEffect(int/Enum idx)
			+StartStopMusic()
		}
		
		class UIManager{
		}

		class DialogueController{
		}

		class EntryChecker{
			<<static>>
			+CheckEntry(Creature toTest) bool
		}

		class Creature{
		}
		
	
		class SettingsMenu{
			+Sensitivity
			+AudioVolume
		}
```