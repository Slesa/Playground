using System;

namespace Godot.IcsEditor.Ui.Events
{
    public class SubscriptionToken : IEquatable<SubscriptionToken>
    {
        readonly Guid _token;

        public SubscriptionToken()
        {
            _token = Guid.NewGuid();
        }

        public bool Equals(SubscriptionToken other)
        {
            return other != null && Equals(_token, other._token);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as SubscriptionToken);
        }

        public override int GetHashCode()
        {
            return _token.GetHashCode();
        }
    }
}