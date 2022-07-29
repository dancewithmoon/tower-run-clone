using ScriptableObjects;

namespace Gameplay.Generators
{
    public interface ILevelGenerator
    {
        public void Generate(LevelParameters parameters);
    }
}
