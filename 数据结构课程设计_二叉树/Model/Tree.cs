using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据结构课程设计_二叉树.Model
{
    public class Node : INotifyPropertyChanged
    {
        ObservableCollection<Node> childNodes;
        Node Lchild;
        Node Rchild;
        string Text;
        string Type;
        string Ltag;
        string Rtag;
        string Visual;
        string Switch;
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public Node(string Text)
        {
            this.Text = Text;
        }

        public Node Copy(Node t)
        {
            Node newnode;
            if (t == null)
            {
                return null;
            }
            else
            {
                newnode = new Node();
                newnode.childNodes = new ObservableCollection<Node>();
                newnode.text = t.text;
                newnode.rtag = t.rtag;
                newnode.ltag = t.ltag;
                newnode.visual = t.visual;
                newnode.enable = t.enable;
                newnode.type = t.type;
                newnode.lchild = Copy(t.lchild);
                newnode.rchild = Copy(t.rchild);
                if (newnode.lchild!=null)
                {
                    newnode.childNodes.Add(newnode.lchild);
                }
                if (newnode.rchild != null)
                {
                    newnode.childNodes.Add(newnode.rchild);
                }
                return newnode;
            }
        }
        public Node()
        {
        }
        public Node lchild
        {
            get { return Lchild; }
            set { Lchild = value; OnPropertyChanged("lchild"); }
        }
        public Node rchild
        {
            get { return Rchild; }
            set { Rchild = value; OnPropertyChanged("rchild"); }
        }
        public ObservableCollection<Node> ChildNodes
        {
            get
            {
                if (childNodes == null)
                    childNodes = new ObservableCollection<Node>();
                return childNodes;
            }
            set
            {
                childNodes = value;
            }
        }
        public string type
        {
            get { return Type; }
            set { Type = value; OnPropertyChanged("type"); }
        }
        public string visual
        {
            get { return Visual; }
            set { Visual = value; OnPropertyChanged("visual"); }
        }
        public string enable
        {
            get { return Switch; }
            set { Switch = value; OnPropertyChanged("enable"); }
        }
        public string text
        {
            get { return Text; }
            set { Text = value; OnPropertyChanged("text"); }
        }
        public string ltag
        {
            get { return Ltag; }
            set { Ltag = value; OnPropertyChanged("ltag"); }
        }
        public string rtag
        {
            get { return Rtag; }
            set { Rtag = value; OnPropertyChanged("rtag"); }
        }
    }
}
