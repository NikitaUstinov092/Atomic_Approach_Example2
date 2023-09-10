namespace System.Declarative.Scripts.Utils
{
    public static class DelegateUtils
    {
        public static void Dispose(ref System.Action action)
        {
            if (action == null)
            {
                return;
            }

            var delegates = action.GetInvocationList();
            foreach (var @delegate in delegates)
            {
                action -= (Action) @delegate;
            }

            action = null;
        }

        public static void Dispose<T>(ref T @delegate) where T : Delegate
        {
            if (@delegate == null)
            {
                return;
            }

            foreach (Delegate value in @delegate.GetInvocationList())
            {
                @delegate = (T) Delegate.Remove(@delegate, value);
            }

            @delegate = null;
        }

        public static void Dispose<T>(ref System.Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            var delegates = action.GetInvocationList();
            foreach (var @delegate in delegates)
            {
                action -= (Action<T>) @delegate;
            }

            action = null;
        }
    }
}