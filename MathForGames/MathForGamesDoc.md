## Code:

#### Classes and functions

**File**: Engine.cs

**Attributes**

         Name: CurrentSceneIndex
             Description: Gets & Sets _currentSceneIndex.
             Type: [Property] public static int

         Name: DefaultColor
             Description: Gets & Sets the console color to white.
             Type: [Property] public static ConsoleColor

         Name: Engine()
             Description: Initializes scenes array.
             Type: [Constructor] public

         Name: GetScene(int index)
             Description: Returns the scene at the index given, Returns an empty scene if index is out of bounds.
             Type: public static Scene

         Name: GetCurrentScene()
             Description: Returns a scene that's at the index of the current scene index.
             Type: public static Scene

         Name: AddScene(Scene scene)
             Description: Adds the given scene to the array of scenes.
             Type: public static int

         Name: RemoveScene(Scene scene)
             Description: Finds the instance of the scene given inside of the array.
             Type: public static bool

         Name: SetCurrentScene(int index)
             Description: Sets current scene in-game to be the scene at the given index.
             Type: public static void

         Name: GetScenes(int index)
             Description: .
             Type: public static Scene

         Name: GetKeyDown(int key)
             Description: Returns true while a key is being pressed.
             Type: public static bool

         Name: GetKeyPressed(int key)
             Description: Returns true if key was pressed once.
             Type: public static bool

         Name: Start()
             Description: Performed once when the game begins.
             Type: public void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public void

         Name: Draw()
             Description: Used to display objects and other info on the screen.
             Type: public void

         Name: End()
             Description: Performed once when the game ends.
             Type: public void

         Name: Run()
             Description: Handles all of the main game logic including the main game loop.
             Type: public void

**File**: Actor.cs

**Attributes**

         Name: Started
             Description: Gets & Sets _started.
             Type: [Property] public bool

         Name: Parent
             Description: Gets & Sets _parent.
             Type: [Property] public Actor

         Name: Forward
             Description: Gets current facing, Sets new facing.
             Type: [Property] public Vector2

         Name: GlobalPosition
             Description: Gets _globalPosition.
             Type: [Property] public Vector2

         Name: LocalPosition
             Description: Gets & Sets _localPosition.
             Type: [Property] public Vector2

         Name: Velocity
             Description: Gets & Sets _velocity.
             Type: [Property] public Vector2

         Name: MaxSpeed
             Description: Gets & Sets _maxSpeed.
             Type: [Property] public float

         Name: Acceleration
             Description: Gets & Sets _acceleration.
             Type: [Property] protected Vector2

         Name: Actor(float y, float x)
             Description: Initializes Actor variables.
             Type: [Constructor] public

         Name: Actor(float x, float y, float collisionRadius)
             Description: Initializes Actor variables.
             Type: [Constructor] public

         Name: AddChild(Actor child)
             Description: .
             Type: public void

         Name: RemoveChild(Actor child)
             Description: .
             Type: public bool

         Name: SetTranslate(Vector2 position)
             Description: .
             Type: public void

         Name: SetRotation(float radians)
             Description: .
             Type: public void

         Name: Rotate(float radians)
             Description: Increases angle of rotation by the given amount.
             Type: public void

         Name: SetScale(float x, float y)
             Description: .
             Type: public void

         Name: CheckCollision(Actor[] actor)
             Description: Checks to see if this actor overlaps with another.
             Type: public virtual bool

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public virtual void

         Name: AddCollisionTarget(Actor actor)
             Description: Adds given actor to an array.
             Type: public void

         Name: UpdateTransforms()
             Description: Updates global transform to the combination of the parent & local transforms, Updates transforms for all children of this actor.
             Type: private void

         Name: UpdateFacing()
             Description: Updates actors forward vector to the last direction it moved in.
             Type: public virtual void

         Name: LookAt(Vector2 position)
             Description: .
             Type: public void

         Name: Start()
             Description: Performed once when the game starts.
             Type: public virtual void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public virtual void

         Name: Draw()
             Description: Used to display objects and other info on the screen.
             Type: public virtual void

         Name: End()
             Description: Performed once when the game ends.
             Type: public virtual void

**File**: Player.cs : Actor

**Attributes**

         Name: Speed
             Description: Gets & Sets _speed.
             Type: [Property] public float

         Name: Player(float x, y) : base()
             Description: Initializes Player variables.
             Type: [Contructor] public

         Name: Player(float x, float y, float collisionRadius) : base()
             Description: Initializes Player variables.
             Type: [Contructor] public

         Name: Start()
             Description: Performed once when the game starts.
             Type: public override void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public override void

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public override void

         Name: DrawWinText()
             Description: Displays text once a player finishes the game.
             Type: public void

**File**: Enemy.cs : Actor

**Attributes**

         Name: Speed
             Description: Gets & Sets _speed.
             Type: [Property] public float

         Name: Target
             Description: Gets & Sets _target.
             Type: [Property] public Actor

         Name: Enemy(float x, float y, Vector2 patrolPointA, Vector2 patrolPointB) : base()
             Description: Initializes Enemy variables.
             Type: [Constructor] public Enemy

         Name: Enemy(float x, float y, float collisionRadius, Vector2 patrolPointA, Vector2 patrolPointB) : base()
             Description: Initializes Enemy variables.
             Type: [Constructor] public Enemy

         Name: CheckTargetInSight(float maxAngle, float maxDistance)
             Description: Checks if the target is within the given angle & distance. Returns false if target hasn't set. Both angle & distance are inclusive.
             Type: public bool

         Name: UpdatePatrolLocation()
             Description: Updates the current location the enemy is traveling to once its reached a patrol point.
             Type: private void

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public override void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public override void

         Name: Draw()
             Description: Used to display objects and other info on the screen.
             Type: public override void

**File**: Partner.cs : Actor

**Attributes**

         Name: Speed
             Description: Gets & Sets _speed.
             Type: [Property] public float

         Name: Target
             Description: Gets & Sets _target.
             Type: [Property] public Actor

         Name: Partner(float x, y) : base()
             Description: Initializes Enemy variables.
             Type: [Constructor] public Partner

         Name: Partner(float x, y, float collisionRadius) : base()
             Description: Initializes Enemy variables.
             Type: [Constructor] public Partner

         Name: CheckTargetInSight(float maxAngle, float maxDistance)
             Description: Checks if the target is within the given angle & distance. Returns false if target hasn't set. Both angle & distance are inclusive.
             Type: public bool

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public override void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public override void

**File**: Goal.cs : Actor

**Attributes**

         Name: Goal(float x, float y, Actor player) : base()
             Description: Initializes Goal variables.
             Type: [Constructor] public Goal

         Name: Goal(float x, float y, float collisionRadius, Actor player) : base()
             Description: Initializes Goal variables.
             Type: [Constructor] public Goal

         Name: CheckPlayerDistance()
             Description: Checks to see if the player is in range of the goal.
             Type: private bool

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public override void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public override void

         Name: Draw()
             Description: Used to display objects and other info on the screen.
             Type: public override void

**File**: Bullet.cs

**Attributes**

         Name: Speed
             Description: Gets & Sets _speed.
             Type: [Property] public float

         Name: Bullet(float x, float y)
             Description: Initializes Bullet variables.
             Type: [Constructor] public

         Name: Bullet(float x, float y, float collisionRadius)
             Description: Initializes Bullet variables.
             Type: [Constructor] public

         Name: CreateBullets(int numBullets, Actor actor)
             Description: Creates & Adds Bullets to the scene based on number given.
             Type: public static void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public override void

         Name: Draw()
             Description: Used to display objects and other info on the screen.
             Type: public override void

**File**: Scene.cs

**Attributes**

         Name: World
             Description: Gets & Sets _transform.
             Type: [Property] public Matrix3

         Name: Started
             Description: .
             Type: [Property] public bool

         Name: Scene()
             Description: Initializes Scene variables.
             Type: [Contructor]

         Name: AddActor(Actor actor)
             Description: .
             Type: public void

         Name: AddActor(Actor actor, int number)
             Description: .
             Type: [Overload] public void

         Name: RemoveActor(int index)
             Description: .
             Type: public bool

         Name: RemoveActor(Actor actor)
             Description: .
             Type: [Overload] public bool

         Name: Start()
             Description: Performed once the game begins.
             Type: public virtual void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public virtual void

         Name: Draw()
             Description: Used to display objects and other info on the screen.
             Type: public virtual void

         Name: End()
             Description: Performed once the game ends.
             Type: public virtual void

**File**: GameManager.cs

**Attributes**

         Name: GameOver
             Description: Gets & Sets _gameOver.
             Type: [Property] public static bool

         Name: Counter()
             Description: Updates text to show goal count.
             Type: public static void

         Name: CheckWin()
             Description: Invokes functions if conditions are met.
             Type: public static void