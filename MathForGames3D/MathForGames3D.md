## Code:

#### Classes and functions

**File**: Engine.cs

**Attributes**

         Name: Engine()
             Description: Initializes scene array.
             Type: [Constructor] public

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
             Description: Returns true if key was pressed more than once.
             Type: public static bool

         Name: GetKeyPressed(int key)
             Description: Returns true if key was pressed once.
             Type: public static bool

         Name: Start()
             Description: Performed once when the game begins.
             Type: private void

         Name: Update(float deltaTime)
             Description: Repeated until the game ends.
             Type: private void

         Name: Draw()
             Description: Used to display objects and other info on the screen.
             Type: private void

         Name: End()
             Description: Performed once when the game ends.
             Type: private void

         Name: Run()
             Description: Handles all of the main game logic including the main game loop.
             Type: public void

**File**: Actor.cs

**Attributes**

         Name: Started
             Description: Gets & Sets _started.
             Type: [Property] public bool

         Name: Forward
             Description: Gets current facing, Sets new facing.
             Type: [Property] public Vector3

         Name: GlobalPosition
             Description: Gets _globalPosition.
             Type: [Property] public Vector3

         Name: LocalPosition
             Description: Gets & Sets _localPosition.
             Type: [Property] public Vector3

         Name: Velocity
             Description: Gets & Sets _velocity.
             Type: [Property] public Vector3

         Name: MaxSpeed
             Description: Gets & Sets _maxSpeed.
             Type: [Property] public float

         Name: Acceleration
             Description: Gets & Sets _acceleration.
             Type: [Property] protected Vector3

         Name: Actor(float x, y, z, float collisionRadius)
             Description: Initializes Actor variables.
             Type: [Constructor] public

         Name: Actor(float x, y, z, Color rayColor, Shape shape, float collisionRadius)
             Description: Initializes Actor variables.
             Type: [Constructor] public

         Name: AddChild(Actor child)
             Description: .
             Type: public void

         Name: RemoveChild(int index)
             Description: .
             Type: public bool

         Name: RemoveChild(Actor child)
             Description: .
             Type: [Overload] public bool

         Name: SetScale(Vector3 scale)
             Description: .
             Type: public void

         Name: Scale(Vector3 scale)
             Description: .
             Type: public void

         Name: SetRotationX(float radians)
             Description: Sets rotation angle to the given value in radians on the X axis.
             Type: public void

         Name: SetRotationY(float radians)
             Description: Sets rotation angle to the given value in radians on the Y axis.
             Type: public void

         Name: SetRotationZ(float radians)
             Description: Sets rotation angle to the given value in radians on the Z axis.
             Type: public void

         Name: Rotate(float radians)
             Description: Increases angle of rotation by the given amount.
             Type: public void

         Name: UpdateFacing()
             Description: Updates actors forward vector to the last direction it moved in.
             Type: protected void

         Name: UpdateGlobalTransform()
             Description: Updates global transform to the combination of the parent & local transforms, Updates transforms for all children of this actor.
             Type: private void

         Name: CheckCollision(Actor[] actor)
             Description: Checks if given actor is in a certain radius, if true calls OnCollision & returns true.
             Type: public virtual bool

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public virtual void

         Name: AddCollisionTarget(Actor actor)
             Description: Adds given actor to an array.
             Type: public void

         Name: LookAt(Vector3 position)
             Description: .
             Type: public void

         Name: Start()
             Description: starts.
             Type: public virtual void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public virtual void

         Name: Draw()
             Description: Used to display objects and other info on the screen.
             Type: public virtual void

         Name: DrawShape()
             Description: .
             Type: private void

         Name: End()
             Description: Performed once the game ends.
             Type: public virtual void

**File**: Player.cs : Actor

**Attributes**

         Name: Speed
             Description: Gets & Sets _speed.
             Type: [Property] public float

         Name: Player(float x, y, z, float collisionRadius) : base()
             Description: Initializes Player variables.
             Type: [Constructor] public 

         Name: Player(float x, y, z, Color rayColor, Shape shape, float collisionRadius) : base()
             Description: Initializes Player variables.
             Type: [Constructor] public

         Name: Start()
             Description: Performed once the game begins.
             Type: public override void

         Name: Update(float deltaTime)
             Description: Called every frame.
             Type: public override void

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public override void

         Name: DrawWinText()
             Description: .
             Type: public void

**File**: Enemy.cs : Actor

**Attributes**

         Name: Target
             Description: Gets & Sets _target.
             Type: [Property] public Actor

         Name: Enemy(float x, y, z, float collisionRadius) : base()
             Description: Initializes Enemy variables.
             Type: [Constructor] public Enemy

         Name: Enemy(float x, y, z, Color rayColor, Shape shape, float collisionRadius) : base()
             Description: Initializes Enemy variables.
             Type: [Constructor] public Enemy

         Name: AddEnemy(int enemyNum, Actor partner, Actor goal, Scene scene)
             Description: .
             Type: public static void

         Name: CheckTargetInSight(float maxAngle, float maxDistance)
             Description: Checks if the target is within the given angle & distance. Returns false if target hasn't set. Both angle & distance are inclusive.
             Type: public bool

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public override void

         Name: Start()
             Description: .
             Type: public override void

         Name: Update(float deltaTime)
             Description: .
             Type: public override void

**File**: Partner.cs : Actor

**Attributes**

         Name: Partner(float x, y, z, float collisionRadius) : base()
             Description: Initializes Enemy variables.
             Type: [Constructor] public Partner

         Name: Partner(float x, y, z, Color rayColor, Shape shape, float collisionRadius) : base()
             Description: Initializes Enemy variables.
             Type: [Constructor] public Partner

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public override void

         Name: Update(float deltaTime)
             Description: .
             Type: public override void

**File**: Goal.cs : Actor

**Attributes**

         Name: Goal(float x, y, z, float collisionRadius) : base()
             Description: Initializes Goal variables.
             Type: [Constructor] public Goal

         Name: Goal(float x, y, z, Color rayColor, Shape shape, float collisionRadius) : base()
             Description: Initializes Goal variables.
             Type: [Constructor] public Goal

         Name: Update(float deltaTime)
             Description: .
             Type: public override void

         Name: OnCollision(Actor[] other)
             Description: .
             Type: public override void

**File**: GameManager.cs

**Attributes**

         Name: GameOver
             Description: Gets & Sets _gameOver.
             Type: public static bool

         Name: CheckWin()
             Description: .
             Type: public static void