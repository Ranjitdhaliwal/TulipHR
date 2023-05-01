namespace TulipHR.API.Models
{
    /// <summary>
    /// A DTO to show positions hierarchy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OrgTreeNode<T>
    {
        /// <summary>
        /// PositionDTO object containing detail of position and employee.
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// List of positions under current node
        /// </summary>
        public List<OrgTreeNode<T>> Children { get; set; } = new List<OrgTreeNode<T>>();

        public OrgTreeNode(T data)
        {
            Data = data;
        }

        public void AddChild(OrgTreeNode<T> child)
        {
            Children.Add(child);
        }
    }
}
