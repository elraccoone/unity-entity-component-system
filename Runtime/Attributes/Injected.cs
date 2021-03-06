namespace ElRaccoone.EntityComponentSystem {

  /// Describes a injectedable system, service or controller.
  [System.AttributeUsage (System.AttributeTargets.Field)]
  public class Injected : System.Attribute {

    /// Sets the attributes values on an object.
    public static void SetAttributeValues (System.Object target) {
      var _fields = target.GetType ().GetFields (
        System.Reflection.BindingFlags.Instance |
        System.Reflection.BindingFlags.NonPublic);
      foreach (var _field in _fields)
        if (System.Attribute.GetCustomAttribute (_field, typeof (Injected)) != null)
          foreach (var _fieldInterface in _field.FieldType.GetInterfaces ()) {
            if (_fieldInterface == typeof (IEntitySystem))
              _field.SetValue (target, Controller.Instance.GetSystem (_field.FieldType));
            else if (_fieldInterface == typeof (IService))
              _field.SetValue (target, Controller.Instance.GetService (_field.FieldType));
            else if (_fieldInterface == typeof (IController))
              _field.SetValue (target, Controller.Instance);
          }
    }
  }
}