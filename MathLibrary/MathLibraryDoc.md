## Code:

#### Classes and functions

**File**: Vector2.cs

**Attributes**

         Name: X
             Description: Gets & Sets _x.
             Type: public float Property

         Name: Y
             Description: Gets & Sets _y.
             Type: public float Property

         Name: Magnitude
             Description: Gets/Returns Magnitude of a vector.
             Type: public float Property

         Name: Normalized
             Description: Gets/Returns a vector Normalized.
             Type: public Vector2 Property

         Name: Vector2()
             Description: Initializes Vector2 variables.
             Type: public Vector2 contructor

         Name: public Vector2(float x, float y)
             Description: Initializes Vector2 variables.
             Type: public Vector2 contructor

         Name: Normalize(Vector2 vector)
             Description: Returns normalized version of the vector passed in.
             Type: public static Vector2

         Name: DotProduct(Vector2 lhs, Vector2 rhs)
             Description: Returns dot product of two given vectors.
             Type: public static float

         Name: operator +(Vector2 lhs, Vector2 rhs)
             Description: Adds two vectors.
             Type: public static Vector2

         Name: operator -(Vector2 lhs, Vector2 rhs)
             Description: Subtracts two vectors.
             Type: public static Vector2

         Name: operator *(Vector2 lhs, float scalar)
             Description: multiplies vector by a scalar.
             Type: public static Vector2

         Name: operator /(Vector2 lhs, float scalar)
             Description: divides vector by a scalar.
             Type: public static Vector2

**File**: Vector3.cs

**Attributes**

         Name: X
             Description: Gets & Sets _x.
             Type: public float Property

         Name: Y
             Description: Gets & Sets _y.
             Type: public float Property

         Name: Z
             Description: Gets & Sets _z.
             Type: public float Property

         Name: Magnitude
             Description: Gets/Returns Magnitude of a vector.
             Type: public float Property

         Name: Normalized
             Description: Gets/Returns a vector Normalized.
             Type: public Vector3 Property

         Name: Vector3()
             Description: Initializes Vector3 variables.
             Type: public Vector3 contructor

         Name: public Vector3(float x, float y)
             Description: Initializes Vector3 variables.
             Type: public Vector3 contructor

         Name: Normalize(Vector3 vector)
             Description: Returns normalized version of the vector passed in.
             Type: public static Vector3

         Name: DotProduct(Vector3 lhs, Vector3 rhs)
             Description: Returns dot product of two given vectors.
             Type: public static float

         Name: operator +(Vector3 lhs, Vector3 rhs)
             Description: Adds two vectors.
             Type: public static Vector3

         Name: operator -(Vector3 lhs, Vector3 rhs)
             Description: Subtracts two vectors.
             Type: public static Vector3

         Name: operator *(Vector3 lhs, float scalar)
             Description: multiplies vector by a scalar.
             Type: public static Vector3

         Name: operator *(float scalar, Vector3 lhs)
             Description: multiplies vector by a scalar.
             Type: public static overload Vector3

         Name: operator /(Vector3 lhs, float scalar)
             Description: divides vector by a scalar.
             Type: public static Vector3

**File**: Vector4.cs

**Attributes**

         Name: X
             Description: Gets & Sets _x.
             Type: public float Property

         Name: Y
             Description: Gets & Sets _y.
             Type: public float Property

         Name: Z
             Description: Gets & Sets _z.
             Type: public float Property

         Name: W
             Description: Gets & Sets _w.
             Type: public float Property

         Name: Magnitude
             Description: Gets/Returns Magnitude of a vector.
             Type: public float Property

         Name: Normalized
             Description: Gets/Returns a vector Normalized.
             Type: public Vector4 Property

         Name: Vector4()
             Description: Initializes Vector4 variables.
             Type: public Vector4 contructor

         Name: public Vector4(float x, float y)
             Description: Initializes Vector3 variables.
             Type: public Vector4 contructor

         Name: Normalize(Vector4 vector)
             Description: Returns normalized version of the vector passed in.
             Type: public static Vector4

         Name: DotProduct(Vector4 lhs, Vector4 rhs)
             Description: Returns dot product of two given vectors.
             Type: public static float

         Name: CrossProduct(Vector4 lhs, Vector4 rhs)
             Description: Returns dot product of two given vectors.
             Type: public static float

         Name: operator +(Vector4 lhs, Vector4 rhs)
             Description: Adds two vectors.
             Type: public static Vector4

         Name: operator -(Vector4 lhs, Vector4 rhs)
             Description: Subtracts two vectors.
             Type: public static Vector4

         Name: operator *(Vector4 lhs, float scalar)
             Description: multiplies vector by a scalar.
             Type: public static Vector4

         Name: operator *(float scalar, Vector4 lhs)
             Description: multiplies vector by a scalar.
             Type: public static overload Vector4

         Name: operator /(Vector4 lhs, float scalar)
             Description: divides vector by a scalar.
             Type: public static Vector4

**File**: Matrix3.cs

**Attributes**

         Name: Matrix3()
             Description: Initilizes Matrix3 variables.
             Type: public Matrix3 contructor

         Name: Matrix3(float m11, float m12, float m13,
                       float m21, float m22, float m23,
                       float m31, float m32, float m33)
             Description: Initilizes Matrix3 variables.
             Type: public Matrix3 contructor

         Name: CreateRotation(float radians)
             Description: Creates a new Matrix that's rotated by the given radians.
             Type: public static Matrix3

         Name: CreateTranslation(Vector2 position)
             Description: Creates a new Matrix that's translated by the given value.
             Type: public static Matrix3

         Name: CreateScale(Vector2 scale)
             Description: Creates a new Matrix that's scaled by the given value.
             Type: public static Matrix3

         Name: operator *(Matrix3 lhs, Matrix3 rhs)
             Description: Multiplies two matrices.
             Type: public static Vector3

         Name: operator *(Matrix3 lhs, Vector3 rhs)
             Description: Multiplies matrix by a vector.
             Type: public static Vector3

         Name: operator +(Matrix3 lhs, Matrix3 rhs)
             Description: Adds two matrices.
             Type: public static Matrix3

         Name: operator -(Matrix3 lhs, Matrix3 rhs)
             Description: Subtracts two matrices.
             Type: public static Matrix3



**File**: Matrix4.cs

**Attributes**

         Name: Matrix4()
             Description: Initilizes Matrix4 variables.
             Type: public Matrix4 contructor

         Name: Matrix4(float m11, float m12, float m13, float m14,
                       float m21, float m22, float m23, float m24,
                       float m31, float m32, float m33, float m34,
                       float m41, float m42, float m43, float m44)
             Description: Initilizes Matrix4 variables.
             Type: public Matrix4 contructor

         Name: CreateRotationX(float radians)
             Description: Creates a new Matrix that's rotated on the X-axis by the given radians.
             Type: public static Matrix4

         Name: CreateRotationY(float radians)
             Description: Creates a new Matrix that's rotated on the Y-axis by the given radians.
             Type: public static Matrix4

         Name: CreateRotationZ(float radians)
             Description: Creates a new Matrix that's rotated on the Z-axis by the given radians.
             Type: public static Matrix4

         Name: CreateTranslation(Vector4 position)
             Description: Creates a new Matrix that's translated by the given value.
             Type: public static Matrix4

         Name: CreateScale(Vector3 scale)
             Description: Creates a new Matrix that's scaled by the given value.
             Type: public static Matrix4

         Name: operator *(Matrix4 lhs, Matrix4 rhs)
             Description: Multiplies two matrices.
             Type: public static Vector3

         Name: operator *(Matrix4 lhs, Vector4 rhs)
             Description: Multiplies matrix by a vector.
             Type: public static Vector4

         Name: operator +(Matrix4 lhs, Matrix4 rhs)
             Description: Adds two matrices.
             Type: public static Matrix4

         Name: operator -(Matrix4 lhs, Matrix4 rhs)
             Description: Subtracts two matrices.
             Type: public static Matrix4