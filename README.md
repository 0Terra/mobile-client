# Mobile-Client
Unity mobile client for 0Terra

## About
0Terra is AR cloud, based on blockchain technology. [More info](https://www.0terra.com).

Mobile client allows to walk throught world and see lands designs in AR mode. Right now client version contains demo-map, this map will be shown for any lands regardless of land properties.

Also client has a function for adding objects on the land in AR mode. Object will be added to the position of the center of the device screen.

**Note:** *in the future we will provide an editor for land editting. We will support 3d objects, animations, scripts. Also we will provide AR cloud for saving content and giving an access (free or paid - AR tokens will be used) to use content for land-designers.*

## Instalation of the project
#### Just clone this project
Clone the project from [our repository](https://github.com/0Terra/mobile-client).

#### Add 3d party plugins
The application requires several 3d party plugins:

* [GO Map - 3D Map for AR Gaming](https://www.assetstore.unity3d.com/en/#!/content/68889)
* [Unity ARKit Plugin](https://www.assetstore.unity3d.com/en/#!/content/92515)
* [TextMesh Pro](https://www.assetstore.unity3d.com/en/#!/content/84126)

#### Add your own api key for a map provider
* Open scene "Map"
* Select object "Map - 3D"
* Set a key for [Mapzen](https://mapzen.com) or [Mapbox](https://www.mapbox.com) or [OSM](https://www.openstreetmap.org/)

#### Build and launch (IOS)
* Build the project
* Open project in xcode
* Select info.plist
* Add values for [location permission privacies](https://iosdevcenters.blogspot.com/2016/09/infoplist-privacy-settings-in-ios-10.html)

## Playing with the project
You can customize demo-land and object for adding on the land. In addition, you can create your own demo-land, and your own objects.

#### Creating your own objects
Look at ready-to-use objects: WelcomeTab, Fountain, and Cube.
You can find those objects in the folder `Assets/ZeroTerra/EnvironmentObjects`.

You can modify the example as prefer or create your own objects from scratch.

## To create object from scratch

* create empty GameObject on the scene and on the set position with {0,0,0}
* add script `UnityARUserAnchorComponent` into root object
* add any object like a child into root object and set any position and rotation
* set Y coordinate of the object so that the bottom of the object will be in the zero Y coordinate of root object (Y coordinate of root object will be ground).
* save as prefab
* add prefab in field `Object Prefab` in object `AREnvironmentManager` on scene `AREnvironment`

####To add interaction to your object

* add collider into object you want to interact
* add `ARInteractiveObject` into the object
* add your custom script for interaction
* in method `Start()` subscribe to `ARInteractiveObject` events
* add methods to process `ARInteractiveObject` events


#### Creating your own demo-land

To create your demo-land:

* create empty GameObect on the scene on the set position with {0,0,0}
* add script `UnityARUserAnchorComponent` into root object
* add child objects and set local positions (root object will be placed in center of a land)
* save as prefab
* add prefab in field `Scene Root Prefab` in object `AREnvironmentManager` on scene `AREnvironment`

## Feedback and support
For get help and give feedback join our [slack channel](https://0terra.slack.com) or write an to [e-mail](contact@0terra.com). You can also report bugs by opening a [github issue](https://github.com/0Terra/mobile-client/issues).