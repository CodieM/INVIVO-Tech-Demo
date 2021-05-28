# Summary

Hello! Thanks for preparing this project outline, I had a blast putting it together. With each of the requirements, I tried to do just a little bit extra. For example, with the scene transition, I made it so multiple could be added, and these would be used one after the other whenever a scene change occurs. More can be seen in the sections for a given scene.

## Assumptions

- The unique object stipulation meant that there must be at least two unique *per scene* but could be reused across scenes
- The visual effect meant any visual feedback informing of selection 

## Thoought Process

I decided to use unity scenes instead on one scene in order to simplify the changing between scenes, and returning objects to their inactive state when leaving the scene. I also prefer working on simpler heirarchies as it makes changes simpler and less likely to cause adverse effects elsewhere. 

I created a base object prefab to build the other objects off of, making selection of each straightforward even though the behavioour amy be vastly different. An InteractiveObject script contains the basic logic, including the ability to specify a camera view location which the camera will move to when that object is selected.

Objects with more complex behaviours had an additional behaviour child object to hold their logic, which was selectively enable and disabled by the interactive object script.

## Scene Transitions

A scene manager singleton is used to control the logic of moving between scenes, and trigger the transition animation. The animation itself takes a screenshot and hides the new scene that was loaded behind this until the transition animation has completed. 

## Scene 1

The campfire object creates an initial increase of intensity of the realtime lights used for the effect, and these lights move around independently and procedurally to create a **somewhat** natural looking fire effect.

The wanderer (also seen in scene two) uses a navmesh to move to a particular destination, created randomly inside a cicrle around its starting position. This movement is animated as well.

Bouncing boxes simply plays an animation when active.

## Scene 2

Besides the wanderer, these scene also contains a windmill which spins when selected. While selected, clicking drag and release the mouse to spin it manually.

## Scene 3

The Cannon object when selected (and when clicking/tapping after selected) will fire a cannonball which will orbit the planet.

Selecting the planet will cause the camera to orbit around the planet giving a more interesting view of the cannons orbiting the planet.
