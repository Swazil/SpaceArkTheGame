namespace SpaceArk.EngineLibrary
{
    public static class IdCreator
    {
        public static string CreateModuleId()
        {
            string id = string.Format("module_{0}", _moduleCount);
            _moduleCount++;
            return id;
        }

        private static int _moduleCount = 0;
    }
}
