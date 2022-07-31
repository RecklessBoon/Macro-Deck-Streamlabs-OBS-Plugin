using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneItemActions
    {
        public Task CenterOnScreenAsync();
        public Task FitToScreenAsync();
        public Task FlipXAsync();
        public Task FlipYAsync();
        public Task RemoveAsync();
        public Task ResetTransformAsync();
        public Task RotateAsync(double deg);
        public Task SetContentCropAsync();
        public Task SetSettingsAsync(ISceneItemSettings settings);
        public Task SetTransformAsync(ITransform transform);
        public Task SetVisibilityAsync(bool visible);
        public Task StretchToScreenAsync();
    }
}
