namespace Demo.UI
{
    /// <summary>
    /// UI 기본 인터페이스. Canvas 와의 상호작용.
    /// </summary>
    public interface IUI
    {
        /// <summary>
        /// Canvas 의 정렬 순서
        /// </summary>
        int sortingOrder { get; set; }

        /// <summary>
        /// Canvas를 활성화
        /// </summary>
        void Show();

        /// <summary>
        /// Canvas 비활성화
        /// </summary>
        void Hide();
    }
}