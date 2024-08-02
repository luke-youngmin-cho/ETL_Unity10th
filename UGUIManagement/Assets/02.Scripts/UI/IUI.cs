namespace Demo.UI
{
    /// <summary>
    /// UI �⺻ �������̽�. Canvas ���� ��ȣ�ۿ�.
    /// </summary>
    public interface IUI
    {
        /// <summary>
        /// Canvas �� ���� ����
        /// </summary>
        int sortingOrder { get; set; }

        /// <summary>
        /// Canvas�� Ȱ��ȭ
        /// </summary>
        void Show();

        /// <summary>
        /// Canvas ��Ȱ��ȭ
        /// </summary>
        void Hide();
    }
}