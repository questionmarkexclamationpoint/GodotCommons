namespace QuestionMarkExclamationPoint.Commons.Extensions;

using Godot;

// TODO move this to a separate package
public static class GodotExtensions {
    public static void DrawSetOrigin(this Node2D node, Vector2 origin)
        => node.DrawSetTransform(position: origin);

    public static void DrawSetOrigin(this Node2D node) => DrawSetOrigin(node, Vector2.Zero);
}
