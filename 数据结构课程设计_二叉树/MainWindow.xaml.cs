using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 数据结构课程设计_二叉树.Model;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Windows.Threading;

namespace 数据结构课程设计_二叉树
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitialTreeView();
            _in.Visibility = Visibility.Collapsed;
            _pre.Visibility = Visibility.Collapsed;
            _post.Visibility = Visibility.Collapsed;
        }
        Node SaveData;
        async void InitialTreeView()
        {
            Node root = new Node();
            root.ltag = "0";
            root.rtag = "0";
            root.visual = "Visible";
            root.enable = "True";
          /*  Node a = new Node("1");
            a.ltag = "0";
            a.rtag = "0";
            a.type = "l";
            a.visual = "Visible";
            a.enable = "True";
            Node b = new Node("2");
            b.ltag = "0";
            b.rtag = "0";
            b.type = "r";
            b.visual = "Visible";
            b.enable = "True";
            root.lchild = a;
            root.rchild = b;
            Node c = new Node("3");
            c.ltag = "0";
            c.rtag = "0";
            c.type = "l";
            c.visual = "Visible";
            c.enable = "True";
            Node d = new Node("4");
            d.ltag = "0";
            d.rtag = "0";
            d.type = "r";
            d.visual = "Visible";
            d.enable = "True";
            a.lchild = c;
            a.rchild = d;
            Node e = new Node("5");
            e.ltag = "0";
            e.rtag = "0";
            e.type = "l";
            e.visual = "Visible";
            e.enable = "True";
            Node f = new Node("6");
            f.ltag = "0";
            f.rtag = "0";
            f.type = "r";
            f.visual = "Visible";
            f.enable = "True";
            b.lchild = e;
            b.rchild = f;
            a.ChildNodes.Add(c);
            a.ChildNodes.Add(d);
            b.ChildNodes.Add(e);
            b.ChildNodes.Add(f);
            root.ChildNodes.Add(a);
            root.ChildNodes.Add(b);*/
            Node dummy = new Node();
            dummy.ChildNodes.Add(root);
            tree.DataContext = dummy.ChildNodes;
            tree.SetBinding(ItemsControl.ItemsSourceProperty, new Binding());
            await Task.Delay(1);
            DrawLine();
        }
        public List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name || string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }

                childList.AddRange(GetChildObjects<T>(child, ""));
            }
            return childList;
        }

        private void DrawLine()
        {
            WhiteBoard.Children.Clear();
            List<TreeViewItem> collection = GetChildObjects<TreeViewItem>(tree, "");
            foreach (TreeViewItem the in collection)
            {
                Node temp = the.DataContext as Node;
                foreach (TreeViewItem parent in collection)
                {
                    Node tempparent = parent.DataContext as Node;
                    if (tempparent.rchild == temp&& tempparent.rtag=="0")
                    {
                        var thePoint = the.TransformToAncestor(tree).Transform(new Point(0, 0));
                        var parentPoint = parent.TransformToAncestor(tree).Transform(new Point(0, 0));
                        Line lineRight = new Line { X1 = parentPoint.X + parent.ActualWidth / 2, Y1 = thePoint.Y - 9, X2 = thePoint.X + the.ActualWidth / 2, Y2 = thePoint.Y + 9, Stroke = Brushes.Black };
                        WhiteBoard.Children.Add(lineRight);
                    }
                    else if (tempparent.lchild == temp&&tempparent.ltag=="0")
                    {
                        var thePoint = the.TransformToAncestor(tree).Transform(new Point(0, 0));
                        var parentPoint = parent.TransformToAncestor(tree).Transform(new Point(0, 0));
                        Line lineLeft = new Line { X1 = thePoint.X + the.ActualWidth / 2, Y1 = thePoint.Y + 9, X2 = parentPoint.X + parent.ActualWidth / 2, Y2 = thePoint.Y - 9, Stroke = Brushes.Black };
                        WhiteBoard.Children.Add(lineLeft);
                    }
                }
            }

            foreach (TreeViewItem the in collection)
            {
                Node temp = the.DataContext as Node;
                foreach (TreeViewItem parent in collection)
                {
                    Node tempparent = parent.DataContext as Node;
                    if (tempparent.rchild == temp && tempparent.rtag=="1")
                    {
                        var thePoint = the.TransformToAncestor(tree).Transform(new Point(0, 0));
                        var parentPoint = parent.TransformToAncestor(tree).Transform(new Point(0, 0));
                        Line line = new Line { X1 = parentPoint.X + parent.ActualWidth / 2 + 21, Y1 = parentPoint.Y + 25, X2 = thePoint.X + the.ActualWidth / 2, Y2 = thePoint.Y + 25, Stroke = Brushes.Red }; 
                        WhiteBoard.Children.Add(line);
                        Ellipse end = new Ellipse { Width = 5,Height=5,Fill= Brushes.Red};
                        end.SetValue(Canvas.LeftProperty, line.X2-3);
                        end.SetValue(Canvas.TopProperty, line.Y2-3);
                        WhiteBoard.Children.Add(end);
                    }
                    else if (tempparent.lchild == temp && tempparent.ltag=="1")
                    {
                        var thePoint = the.TransformToAncestor(tree).Transform(new Point(0, 0));
                        var parentPoint = parent.TransformToAncestor(tree).Transform(new Point(0, 0));
                        Line line= new Line { X1 = parentPoint.X + parent.ActualWidth / 2 - 21, Y1 = parentPoint.Y + 25, X2 = thePoint.X + the.ActualWidth / 2, Y2 = thePoint.Y + 25, Stroke = Brushes.Red };
                        WhiteBoard.Children.Add(line);
                        Ellipse end = new Ellipse { Width = 6, Height = 6, Fill = Brushes.Red };
                        end.SetValue(Canvas.LeftProperty, line.X2-3);
                        end.SetValue(Canvas.TopProperty, line.Y2-3);
                        WhiteBoard.Children.Add(end);
                    }
                }
            }
        }


        private void PreOrder(Node t)
        {
            if (t != null)
            {
                if (t.text != "")
                {
                    result.Text += t.text + " ";
                    foreach (Node n in t.ChildNodes)
                    {
                        if (n.type == "l")
                        {
                            PreOrder(n);
                        }
                    }
                    foreach (Node n in t.ChildNodes)
                    {
                        if (n.type == "r")
                        {
                            PreOrder(n);
                        }
                    }
                }
            }
        }

        private void MidOrder(Node t)
        {
            if (t.text != "")
            {
                foreach (Node n in t.ChildNodes)
                {
                    if (n.type == "l")
                    {
                        MidOrder(n);
                    }
                }
                result.Text += t.text + " ";
                foreach (Node n in t.ChildNodes)
                {
                    if (n.type == "r")
                    {
                        MidOrder(n);
                    }
                }
            }
        }

        private void PostOrder(Node t)
        {
            if (t.text != "")
            {
                foreach (Node n in t.ChildNodes)
                {
                    if (n.type == "l")
                    {
                        PostOrder(n);
                    }
                }
                foreach (Node n in t.ChildNodes)
                {
                    if (n.type == "r")
                    {
                        PostOrder(n);
                    }
                }
                result.Text += t.text + " ";
            }
        }

        private int Countleafs(Node t)
        {
            if (t.text != "")
            {
                if (t.ChildNodes.Count == 0)
                {
                    return 1;
                }
                else if(t.ChildNodes.Count == 1)
                {
                    return Countleafs(t.ChildNodes[0]);
                }
                else if (t.ChildNodes.Count == 2)
                {
                    return Countleafs(t.ChildNodes[0])+ Countleafs(t.ChildNodes[1]);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        Node Pre;
        private void preorder_threading(Node t)
        {
            if (t== null )
            {
                return;
            }
            if (t.lchild==null)
            {
                t.lchild=Pre;
                t.ltag = "1";
            }
            if (Pre != null && Pre.rchild == null)
            {
                Pre.rchild = t;
                Pre.rtag = "1";
            }
            Pre = t;
            if (t.ltag=="0")
            {
                preorder_threading(t.lchild);
            }
            if (t.rtag == "0")
            {
                preorder_threading(t.rchild);
            }
        }

        Node Pre2;
        private void inorder_threading(Node t)
        {
            if (t == null )
            {
                return;
            }
            inorder_threading(t.lchild);
            if (t.lchild == null )
            {
                t.lchild = Pre2;
                t.ltag = "1";
            }
            if (Pre2 != null && Pre2.rchild == null)
            {
                Pre2.rchild = t;
                Pre2.rtag = "1";
            }
            Pre2 = t;
            inorder_threading(t.rchild);
        }

        Node Pre3;
        private void postorder_threading(Node t)
        {
            if (t!=null) {
                postorder_threading(t.lchild);
                postorder_threading(t.rchild);
                if (t.lchild == null )
                {
                    t.lchild = Pre3;
                    t.ltag = "1";
                }
                if (Pre3 != null && Pre3.rchild == null)
                {
                    Pre3.rchild = t;
                    Pre3.rtag = "1";
                }
                Pre3 = t;
            }
        }

        private void preorder_visit(Node t)
        {
            if (t==null)
            {
                return;
            }
            Node temp = t;
            while (temp!=null)
            {
                while (temp.lchild!=null && temp.ltag =="0")
                {
                    result.Text += temp.text + " ";
                    temp = temp.lchild;
                }
                result.Text += temp.text + " ";
                if (temp.ltag == "1")
                {
                   temp= temp.rchild;
                }
                while (temp != null)
                {
                    if (temp.lchild != null && temp.ltag == "0")
                    {
                        break;
                    }
                    result.Text += temp.text + " ";
                    temp = temp.rchild;
                }

            }
        }

        private void inorder_visit(Node t)
        {
            if (t == null)
            {
                return;
            }
            Node temp = t;
            while (temp != null)
            {
                while (temp.ltag == "0")
                {
                    temp = temp.lchild;
                }
                result.Text += temp.text + " ";
                while (temp != null&&temp.rtag=="1")
                {
                    temp = temp.rchild;
                    result.Text += temp.text + " ";
                }
                temp = temp.rchild;
            }
        }

       /* private void postorder_visit(Node t)
        {
            if (t != null)
            {
            Node temp = t;
            Node Pre4 = null;
            while (temp != null)
            {
                while (temp.lchild != Pre4 && temp.ltag == "0")
                {
                    temp = temp.lchild;
                }
                while (temp != null && temp.rtag == "1")
                {
                    result.Text += temp.text + " ";
                    Pre4 = temp;
                    temp = temp.rchild;
                }
                if (Pre4 == t)
                {
                    result.Text += temp.text + " ";
                    return;
                }
                while (temp != null&&temp.rchild==Pre4)
                {
                    result.Text += temp.text + " ";
                    Pre4 = temp;
                    List<TreeViewItem> collection = GetChildObjects<TreeViewItem>(tree, "");
                    foreach (TreeViewItem item in collection)
                    {
                        Node i = item.DataContext as Node;
                        if (i.ChildNodes.Contains(temp))
                        {
                            temp = i;
                        }
                    }
                }
                if (temp!=null && temp.rtag=="0")
                {
                    temp = temp.rchild;
                }
               }
            }
        }*/

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            var node = (sender as Button).DataContext as Node;
            List<TreeViewItem> collection = GetChildObjects<TreeViewItem>(tree, "");
            foreach (TreeViewItem item in collection)
            {
                Node itemnode = item.DataContext as Node;
                if (itemnode.ChildNodes.Contains(node))
                {
                    itemnode.ChildNodes.Remove(node);
                    if (itemnode.lchild==node)
                    {
                        itemnode.lchild = null;
                    }
                    else if (itemnode.rchild == node)
                    {
                        itemnode.rchild = null;
                    }
                    break;
                }
            }
            await Task.Delay(1);
            DrawLine();
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            var node = (sender as Button).DataContext as Node;
            List<TreeViewItem> collection = GetChildObjects<TreeViewItem>(tree, "");
            if (node.text != ""&&node.ChildNodes.Count<2)
            {
                Node a = new Node("");
                a.ltag = "0";
                a.rtag = "0";
                a.type = "l";
                a.visual = "Visible";
                a.enable = "True";
                Node b = new Node("");
                b.ltag = "0";
                b.rtag = "0";
                b.type = "r";
                b.visual = "Visible";
                b.enable = "True";
                node.lchild = a;
                node.rchild = b;
                node.ChildNodes.Add(a);
                node.ChildNodes.Add(b);
            }
            await Task.Delay(1);
            DrawLine();

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawLine();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            Node root = SaveData.Copy(SaveData);
            Node dummy = new Node();
            dummy.ChildNodes.Add(root);
            tree.DataContext = dummy.ChildNodes;
            result.Text += "前序遍历：";
            PreOrder(root);
            result.Text += '\n';
            result.Text += "叶子结点数量：";
            result.Text += Countleafs(root);
            result.Text += '\n';
            preorder_threading(root);
            await Task.Delay(1);
            DrawLine();
            result.Text += "前序线索化遍历：";
            preorder_visit(root);
            result.Text += '\n';
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            Node root = SaveData.Copy(SaveData);
            Node dummy = new Node();
            dummy.ChildNodes.Add(root);
            tree.DataContext = dummy.ChildNodes;
            result.Text += "中序遍历：";
            MidOrder(root);
            result.Text += '\n';
            result.Text += "叶子结点数量：";
            result.Text += Countleafs(root);
            result.Text += '\n';
            inorder_threading(root);
            await Task.Delay(1);
            DrawLine();
            result.Text += "中序线索化遍历：";
            inorder_visit(root);
            result.Text += '\n';
        }

        private async void _post_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            Node root = SaveData.Copy(SaveData);
            Node dummy = new Node();
            dummy.ChildNodes.Add(root);
            tree.DataContext = dummy.ChildNodes;
            result.Text += "后序遍历：";
            PostOrder(root);
            result.Text += '\n';
            result.Text += "叶子结点数量：";
            result.Text += Countleafs(root);
            result.Text += '\n';
            postorder_threading(root);
            await Task.Delay(1);
            DrawLine();
        }

        private async void complete_Click(object sender, RoutedEventArgs e)
        {
            _in.Visibility = Visibility.Visible;
            _pre.Visibility = Visibility.Visible;
            _post.Visibility = Visibility.Visible;
            complete.Visibility = Visibility.Collapsed;
            List<TreeViewItem> collection = GetChildObjects<TreeViewItem>(tree, "");
            foreach (TreeViewItem item in collection)
            {
                Node itemnode = item.DataContext as Node;
                itemnode.visual= "Collapsed";
                itemnode.enable = "False";
            }
            foreach (TreeViewItem the in collection)
            {
                Node temp = the.DataContext as Node;
                if (temp.text == "")
                {
                    foreach (TreeViewItem item in collection)
                    {
                        Node itemnode = item.DataContext as Node;
                        if (itemnode != null && itemnode.ChildNodes.Contains(temp))
                        {
                            itemnode.ChildNodes.Remove(temp);
                            if (itemnode.lchild == temp)
                            {
                                itemnode.lchild = null;
                            }
                            else if (itemnode.rchild == temp)
                            {
                                itemnode.rchild = null;
                            }
                            break;
                        }
                    }
                }
            }
            await Task.Delay(1);
            DrawLine();
            SaveData = GetChildObjects<TreeViewItem>(tree, "")[0].DataContext as Node;
            SaveData = SaveData.Copy(SaveData);
        }

        private async void clear_Click(object sender, RoutedEventArgs e)
        {
            _in.Visibility = Visibility.Collapsed;
            _pre.Visibility = Visibility.Collapsed;
            _post.Visibility = Visibility.Collapsed;
            complete.Visibility = Visibility.Visible;
            Node root = new Node("0");
            root.ltag = "0";
            root.rtag = "0";
            root.visual = "Visible";
            root.enable = "True";
            Node dummy = new Node();
            dummy.ChildNodes.Add(root);
            tree.DataContext = dummy.ChildNodes;
            await Task.Delay(1);
            DrawLine();
            result.Text = "";
        }
    }
}
