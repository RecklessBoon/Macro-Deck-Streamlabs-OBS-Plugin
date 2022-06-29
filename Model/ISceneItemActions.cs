namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneItemActions
    {
        public void CenterOnScreen();
        public void FitToScreen();
        public void FlipX();
        public void FlipY();
        public void Remove();
        public void ResetTransform();
        public void Rotate(double deg);
        public void SetContentCrop();
        public void SetSettings(ISceneItemSettings settings);
        public void SetTransform(ITransform transform);
        public void SetVisibility(bool visible);
        public void StretchToScreen();
    }
}
