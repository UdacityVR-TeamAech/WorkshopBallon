# WorkshopBallon

** Unity 2017.1.1p3, GVR 1.6
** platform: Android 7.0, Sony Xperia X Compact
		
**Interaction:** 
* inflate balloon: clapping Hands, inflate
* deflate balloon: make sound "tsssss" (fast) or "kisses" in the air (fine tune)

Tools used for sound analysis:
* **Models/Mechanics** Balloon: ADI 
  [https://github.com/UdacityVR-TeamAech/ColorChangeByBalloonSize]
* **audacity**: www.audacity.de
* **tone generator**: onlinetonegenerator.com
* **pyhton**: stft.zip
* **MicrophoneInput, Audiovisualization** 
	* MyLittleUnity3D Tutorial on youtube: 
		- https://www.youtube.com/watch?v=AkKuXVPlbcE
		- https://www.youtube.com/watch?v=CdNBsWowRbE&t=328s

## open issues:

**Microphone:** there is a jerk when you select the balloon (selecting the cube under the balloon). 
This comes from the mirophone, because I can only start recording when the mic is ready (otherwise it causes errors).  

**Permissions for Microphone:** requesting user permission (permissions set in manifest) at start of the app. 
Currently, the user has to manually (open Settings, App, select the App, enable mic permission) enable the permission.
For the experience of the end users it would be better to request the permission.

**Interaction:** It is working :), more effective interaction could be generated when using for example a support vector machine, but for
this project I think the interaction works fair enough. Open issue, currently inflate is in steps i+2, deflate i-10. 
Playing around what is the best for the user experience. 

**Model:** the knot of the ballon is not transforming with the ballon (size and position).
