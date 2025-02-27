namespace Assets.Scripts
{
    public static class Utils
    {
        public static float NegativeMult(float num1, float num2)
        {
            if (num1 < 0 && num2 < 0) return -(num1 * num2);
            return num1 * num2;
        }
    }
}
