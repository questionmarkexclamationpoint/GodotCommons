namespace Commons;

using Godot;

public static class GodotExt {
    public static void DrawSetOrigin(this Node2D node, Vector2 origin)
            => node.DrawSetTransform(position: origin);

    public static void DrawSetOrigin(this Node2D node) => DrawSetOrigin(node, Vector2.Zero);
}
