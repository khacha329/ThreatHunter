# ThreatHunter
Repo for my ThreatHunter application made in unity and Kali Linux, deployed on HoloLens2 and awuS036ACH v2 in monitor mode respectively.

# Threat Hunter application for Wi-Fi on HoloLens2 using MRTK, SpatialAwareness, MQTT and kali Linux.

## A demo application to hunt for wifi strength with a spatial mesh visualization with wifi strength.

This project is an example to how a threat hunter application could be used in augmented reality and easily interacted with. The project is made in Unity 2021.3.16f using MRTK 2, Spatial awareness, MQTT and Kali linux. It has the potential to offer these functionalities:

- List detected Wi-Fi IDs in AR
- Hunt for Wi-Fi source by roaming around the space (implemented could be improved)
- Hack Wi-Fi and exploit vulnerabilities (work in progress)

## How to Set-Up this application

1. First download unity 2021.3.16f - https://unity.com/releases/editor/whats-new/2021.3.16
2. Clone this repo and open project in unity
3. Set-up MRTK2 in Unity for HoloLens2 - https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk2/?view=mrtkunity-2022-05 (if you dont have access to a HoloLens2 explore using an emulator - https://learn.microsoft.com/en-us/windows/mixed-reality/develop/advanced-concepts/using-the-hololens-emulator)
4. Once everything is set up and no errors are shown in unity deploy the app to HoloLens2 by first setting up build settings - https://learn.microsoft.com/en-us/windows/mixed-reality/develop/unity/build-and-deploy-to-hololens then using MRTK build window accessible in unity (Mixed Reality -> ToolKit -> Utilities -> Build Window) Then select the scenes you want to build then build and install.
5.Head to your HoloLens2 Device portal(https://learn.microsoft.com/en-us/windows/mixed-reality/develop/advanced-concepts/using-the-windows-device-portal) to deploy the app using Views->Apps then choosing file from .\Builds\WSAPlayer\AppPackages\SUTest\SUTest_1.0.117.0_ARM64_Master_Test (example) and deploy the app
6.Once the app deployed and launched, set up Kali linux on a VM (or boot). Set up the awuS036ACH v2 in monitor mode (https://www.youtube.com/watch?v=C4vhPgL1Ve0) 
7.Install python (pip install python) and mosquitto broker on linux (http://www.steves-internet-guide.com/install-mosquitto-linux/) Then run the Sniffer2.py script (make sure the Hololens2 app is launched before running this script as it publishes to mqtt only the new addresses seen by linux)
8. Select the wifi id you want to hunt and roam around with your antenna and hololens2 until the signal is strong enough to determine the location of the wifi source.

Find here zip file for a set-up unity project on unity 2021.2.16f - https://unioulu-my.sharepoint.com/:u:/g/personal/kchakal22_univ_yo_oulu_fi/ES_6R9mguORPuv69onuvbesB3__LbbrUnMLrRaiYThAFig?e=OkF43y

(link valid till 30 march 2024)

## The application should look like something like this
![Screenshot](Screenshots\UI.jpg)

When hunting a strong strength wifi you would:

![Screenshot](Screenshots\StrongStrength.jpg)

When hunting a weak strength wifi you would:

![Screenshot](Screenshots\WeakStrength.jpg)

## How to tweak this project for your own uses

Since this is an example project, I'd encourage you to clone and rename this project to use for your own puposes.
You will find script files in .\Assets\Scripts.
Kali Linux scripts files in .\Scripts

## Find a bug?

If you found an issue or would like to submit an improvement to this project, please submit an issue using the issues tab above. If you would like to submit a PR with a fix, reference the issue you created!
You will find script files for unity in .\Assets\Scripts.
Kali Linux scripts files in .\Scripts

## Known issues (Work in progress)

This example is a rough template currentissues are:

1. The app currently takes about 2 seconds to refresh the spatial mesh with a 2m query radius which is limited but this can be easily tweaked with in the Spatial Awareness profile which can be found in the heirarchy of the ThreatHunter Scene under MixedReality Toolkit-> Spatial Awareness ->OpenXR Spatial Mesh Observer
