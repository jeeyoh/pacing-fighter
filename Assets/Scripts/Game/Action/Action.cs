﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Data container for an action
/// </summary>
[System.Serializable]
public class Action
{
	/// <summary>
	/// The action's identifier, used for debugging purposes
	/// </summary>
	public string name;

	/// <summary>
	/// The character who is performing this action.
	/// </summary>
	[System.NonSerialized]
	public Character character;

	/// <summary>
	/// The GameObject this action is targetting. May be null if the action does not target any GameObject.
	/// </summary>
	[System.NonSerialized]
	public GameObject targetObject;
	
	/// <summary>
	/// The position this action is targetting. May be zero, if the action does not require a target position.
	/// </summary>
	[System.NonSerialized]
	public Vector2 targetPosition = Vector2.zero;

	/// <summary>
	/// The possible animation sequences played
	/// when the action is performed
	/// </summary>
	public AnimationSequence[] animationSequences = new AnimationSequence[1]{new AnimationSequence()};

	/// <summary>
	/// The hit boxes which can harm the enemy when this move is performed.
	/// </summary>
	public HitBox[] hitBoxes = new HitBox[0];

	/// <summary>
	/// The forces applied on the character that performs this action
	/// </summary>
	public Force[] forces = new Force[0];

	/// <summary>
	/// The events performed right when the action starts being performed. The character which performs
	/// this action is the same one performing these events 
	/// </summary>
	public Brawler.Event[] onStartEvents = new Brawler.Event[0];

	/// <summary>
	/// The combat actions which can cancel this action and be performed instead. Useful for creating combos.
	/// Note that any combat action can be performed after this action. However, if a move is in this list,
	/// it has higher priority, and will be chosen first.
	/// </summary>
	public ActionScriptableObject[] linkableCombatActionScriptableObjects = new ActionScriptableObject[0];
	public Action[] linkableCombatActions = new Action[0];

	/// <summary>
	/// If true, the move can be performed through user input. If false, the move is performed only through code
	/// </summary>
	public bool listensToInput = false;

	/// <summary>
	/// The type of input required to activate the move (tap/swipe)
	/// </summary>
	public InputType inputType;

	/// <summary>
	/// The region to touch to activate the move
	/// </summary>
	public InputRegion inputRegion;

	/// <summary>
	/// The swipe direction required to activate the move.
	/// </summary>
	public SwipeDirection swipeDirection;

	/// <summary>
	/// The sounds which can play when this action is activated
	/// </summary>
	public AudioClip[] startSounds = new AudioClip[0];
	
	/// <summary>
	/// The sounds which can be played when this action hits an adversary
	/// </summary>
	public AudioClip[] impactSounds = new AudioClip[0];

	/// <summary>
	/// If true, this move can be canceled midway to perform another move or action. 
	/// </summary>
	public bool cancelable;

	/// <summary>
	/// If true, this move can interrupt any move, and be performed instead.
	/// It can even cancel a move marked as 'cancelable'
	/// </summary>
	public bool overrideCancelable;

	/// <summary>
	/// Default constructor. Use when the fields will be populated by the inspector.
	/// </summary>
	public Action()
	{
	}

	/// <summary>
	/// Create an Action and copy all the values from the given template
	/// </summary>
	public Action(Action template)
	{
		// Copy the values from the given template
		name = template.name;

		animationSequences = ArrayUtils.Copy<AnimationSequence>(template.animationSequences);
		hitBoxes = ArrayUtils.DeepCopy(template.hitBoxes);
		forces = ArrayUtils.Copy<Force>(template.forces);
		onStartEvents = ArrayUtils.Copy<Brawler.Event>(template.onStartEvents);
		linkableCombatActionScriptableObjects = ArrayUtils.Copy<ActionScriptableObject>(template.linkableCombatActionScriptableObjects);

		listensToInput = template.listensToInput;
		inputType = template.inputType;
		inputRegion = template.inputRegion;
		swipeDirection = template.swipeDirection;

		startSounds = ArrayUtils.Copy<AudioClip>(template.startSounds);
		impactSounds = ArrayUtils.Copy<AudioClip>(template.impactSounds);

		cancelable = template.cancelable;
		overrideCancelable = template.overrideCancelable;
	}


}

/// <summary>
/// A container for a sequence of consecutively-played animations
/// </summary>
[System.Serializable]
public class AnimationSequence
{
	/** The animations which are played consecutively */
	public string[] animations = new string[1]{""};

	/** If true, the last animation in this sequence is set to loop. */
	public bool loopLastAnimation = false;
}

/// <summary>
/// A force applied on the character whilst performing a move 
/// </summary>
[System.Serializable]
public class Force
{
	/// <summary>
	/// Determines whether the force is specified by a velocity or a target position
	/// </summary>
	public ForceType forceType;

	/// <summary>
	/// The velocity at which the character moves. Only used if this force is specified by a velocity.
	/// </summary>
	public Vector2 velocity;

	/// <summary>
	/// Specifies the type of target the character is trying to move towards. Used if 'forceType=Position'
	/// </summary>
	public TargetPosition target = TargetPosition.None;

	/// <summary>
	/// The target position that the force will move an entity to. Only used if 'target==TargetPosition.CustomPosition'
	/// </summary>
	public Vector2 customTargetPosition;

	/// <summary>
	/// The event to perform once the force is done being applied. For instance, if the event requires an action
    /// to be performed, the action is performed by the same entity that was affected by this force
	/// </summary>
	public Brawler.Event onCompleteEvent = new Brawler.Event();

	/// <summary>
	/// If true, the entity performing this action will face towards his TargetPosition when this force is applied
	/// Note: Only applies when 'target != TargetPosition.None'
	/// </summary>
	public bool faceTarget = false;

	/// <summary>
	/// The time at which the force activates
	/// </summary>
	public CastingTime startTime = new CastingTime();
	
	/// <summary>
	/// The amount of time the force is active.
	/// </summary>
	public CastingTime duration = new CastingTime();

}

/// <summary>
/// A camera movement triggered by an action's event
/// </summary>
[System.Serializable]
public class CameraMovement
{
	/// <summary>
	/// The target position the camera will move towards.
	/// </summary>
	public TargetPosition targetPosition;

	/// <summary>
	/// The Transform the camera will try to follow. Used if 'targetPosition == Self'
	/// </summary>
	[HideInInspector]
	public Transform transformToFollow;

	/// <summary>
	/// The position the camera will move towards. Used if the camera must follow a static, non-moving position.
	/// </summary>
	public Vector2 position;

	/// <summary>
	/// The target zoom of the camera.
	/// </summary>
	public float zoom;

	/// <summary>
	/// The speed at which the camera moves to its target position and zoom
	/// </summary>
	public float cameraSpeed = 1.0f;
}

/// <summary>
 /// A slow motion event that can be triggered from an action
 /// </summary>
[System.Serializable]
public class SlowMotion
{
	/// <summary>
	/// The time scale to set the game at when slow motion is active. The lower the number, the slower the speed
	/// </summary>
	public float timeScale = 0.5f;
}

[System.Serializable]
public class ParticleEvent
{
	/// <summary>
	/// The particle effect that is played when the event is triggered.
	/// </summary>
	public ParticleEffect effect;

	/// <summary>
	/// The point at which the particles spawn
	/// </summary>
	public ParticleSpawnPoint spawnPoint;

	/// <summary>
	/// A position offset for the particles, relative to the spawning point. If the spawn
	/// point is set to 'Self', the offset is relative to the entity's facing direction.
	/// That is, if offset = (1,2,0), and the entity is facing left, the x-component is 
	/// flipped directions, and the offset is set to (-1,2,0)
	/// </summary>
	public Vector3 offset;

}

/// <summary>
/// The location where a particle effect is spawned 
/// </summary>
public enum ParticleSpawnPoint
{
	Self
}

/// <summary>
/// Denotes the time at which a certain event starts or lasts.
/// The time can be specified to last as long as an animation, 
/// or simply be specified to last a certain number of frames.
/// </summary>
[System.Serializable]
public class CastingTime
{
	/// <summary>
	/// The type of duration used to specify this time. Does it last as long as an animation, or does it
	/// last a certain number of frames?
	/// </summary>
	public DurationType type;

	/// <summary>
	/// The force will last as long as this animation. The animation is specified by its number, as
	/// given in the "Animation Sequences" dropdown.
	/// </summary>
	public int animationToWaitFor;

	/// <summary>
	/// The amount of time the force is applied, in frames. Only used if 'durationType' is set to 'Frames'
	/// </summary>
	public int nFrames;
}

/// <summary>
/// The type of force applied onto a character. Either he can move at a specified velocity,
/// or move towards a specified location when the force is applied
/// </summary>
public enum ForceType
{
	Velocity,
	Position // Move towards a specified target position
}

/// <summary>
/// Specifies the way in which a duration is specified. Does this duration last as long as an animation,
/// or does it last a certain number of frames?
/// </summary>
public enum DurationType
{
	Frame,
	/** The force lasts as long as the duration of a specified animation */
	WaitForAnimationComplete, 
	/** Use the character's default physics data when making him move. The force will last as long as it takes for his physics values to get him there */
	UsePhysicsData
}

/// <summary>
/// Denotes a target position. This may denot where a character moves towards when a force is applied on him, 
/// or where the camera follows when a CameraMovement event is triggered.
/// </summary>
public enum TargetPosition
{
	TouchedObject,
	TouchedPosition,
	Self,
	CustomPosition,
	None
}

/// <summary>
/// Denotes the type of input required to perform a move (tap or swipe)
/// </summary>
public enum InputType
{
	Click,
	Swipe
}

/// <summary>
/// Denotes the region that needs to be touched to perform a move
/// </summary>
public enum InputRegion
{
	EmptySpace,
	Enemy,
	Self,
	Any
}