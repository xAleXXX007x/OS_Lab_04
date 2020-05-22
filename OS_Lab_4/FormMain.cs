using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Lab_4
{
    public partial class FormMain : Form
    {
        public Dictionary<int, Cluster> disk;
        public List<AbstractFile> files;
        public const int diskSize = 256;
        public int dirCount;
        public int fileCount;
        public Random rand = new Random();

        private TreeNode selectedNode;

        public FormMain()
        {
            InitializeComponent();
            dirCount = 0;
            fileCount = 0;

            disk = new Dictionary<int, Cluster>();
            files = new List<AbstractFile>();

            Directory root = new Directory("Корневая директория");
            root.Id = dirCount++;

            treeView.Nodes[0].Tag = root;
            treeView.Nodes[0].Expand();

            Draw();
        }

        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(bmp);

            int index = 0;

            for (int i = 0; i < pictureBox.Width / 10; i++)
            {
                for (int k = 0; k < pictureBox.Height / 10; k++)
                {
                    Color color = Color.Gray;

                    if (disk.ContainsKey(index))
                    {
                        color = Color.Blue;
                    }

                    if (selectedNode != null)
                    {
                        if (selectedNode.Tag is File)
                        {
                            int id = (selectedNode.Tag as File).Id;

                            if (disk.ContainsKey(index) && FileFromCluster(disk[index]).Id.Equals(id))
                            {
                                color = Color.Red;
                            }
                        } else if (selectedNode.Tag is Directory)
                        {
                            List<int> list = GetDirectoryFileIds(selectedNode.Tag as Directory);

                            if (disk.ContainsKey(index) && list.Contains(FileFromCluster(disk[index]).Id))
                            {
                                color = Color.Red;
                            }
                        }
                    }

                    Brush b = new SolidBrush(color);
                    g.FillRectangle(b, k * 10, i * 10, 8, 8);

                    index++;

                    if (index >= diskSize)
                    {
                        break;
                    }
                }

                if (index >= diskSize)
                {
                    break;
                }
            }

            pictureBox.Image = bmp;
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedNode = e.Node;
                contextMenuStrip.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }

        private void ToolStripAddDirectory_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Добавляем директорию");

            if (selectedNode != null)
            {
                if (selectedNode.Tag is File)
                {
                    MessageBox.Show("Нельзя добавить директорию в файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                TreeNode newNode = selectedNode.Nodes.Add("Новая директория");
                Directory newDir = new Directory("Новая директория");
                newDir.Id = dirCount++;
                newNode.Tag = newDir;

                (selectedNode.Tag as Directory).Content.Add(newDir);

                Draw();
            }
        }

        private void ToolStripAddFile_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Добавляем файл");

            if (selectedNode != null)
            {
                if (selectedNode.Tag is File)
                {
                    MessageBox.Show("Нельзя добавить файл в файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                int size = rand.Next(2, 5);
                TreeNode newNode = selectedNode.Nodes.Add("Новый файл");
                File newFile = new File("Новый файл", size);
                newFile.Id = fileCount++;

                files.Add(newFile);

                newNode.Tag = newFile;

                (selectedNode.Tag as Directory).Content.Add(newFile);

                try
                {
                    LocateFile(newFile);
                } catch (Exception ex)
                {   
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                Draw();
            }
        }

        private List<int> GetDirectoryFileIds(Directory dir)
        {
            List<int> result = new List<int>();
            foreach (var file in dir.Content)
            {
                if (file is Directory)
                {
                    result.AddRange(GetDirectoryFileIds(file as Directory));
                } else if (file is File)
                {
                    result.Add(file.Id);
                }
            }

            return result;
        }

        private List<int> FindFreeSpace(int size)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < diskSize; i++)
            {
                if (!disk.ContainsKey(i))
                {
                    result.Add(i);

                    if (result.Count >= size)
                    {
                        return result;
                    }
                }
            }

            throw new Exception("Диск переполнен");
        }

        private void LocateFile(File file)
        {
            List<int> freeSpace = FindFreeSpace(file.Size);
            Cluster prevCluster = null;

            foreach (int i in freeSpace)
            {
                Cluster cluster = new Cluster();
                cluster.Id = i;
                if (file.Cluster == null)
                {
                    file.Cluster = cluster;
                }

                if (prevCluster != null)
                {
                    prevCluster.Next = cluster;
                }

                disk.Add(i, cluster);

                prevCluster = cluster;
            }
        }

        private AbstractFile FileFromCluster(Cluster cluster)
        {
            foreach (AbstractFile file in files)
            {
                var fileCluster = file.Cluster;

                while (fileCluster != null)
                {
                    if (fileCluster.Id == cluster.Id)
                    {
                        return file;
                    }

                    fileCluster = fileCluster.Next;
                }
            }

            throw new Exception("Файл не найден");
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectedNode = e.Node;
            Draw();
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            if (selectedNode != null)
            {
                AbstractFile file = selectedNode.Tag as AbstractFile;
                List<int> toRemove = new List<int>();

                if (file is File)
                {
                    foreach (var cluster in disk)
                    {
                        if (FileFromCluster(cluster.Value).Id.Equals(file.Id))
                        {
                            toRemove.Add(cluster.Key);
                        }
                    }
                } else if (file is Directory)
                {
                    if (file.Id.Equals(0))
                    {
                        return;
                    }

                    var diskFiles = GetDirectoryFileIds(file as Directory);

                    foreach (var cluster in disk)
                    {
                        if (diskFiles.Contains(FileFromCluster(cluster.Value).Id))
                        {
                            toRemove.Add(cluster.Key);
                        }
                    }
                }


                foreach (var id in toRemove)
                {
                    disk.Remove(id);
                }

                files.Remove(file);
                treeView.Nodes.Remove(selectedNode);

                Draw();
            }
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
            else if (e.Button == MouseButtons.Right)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));
            treeView.SelectedNode = treeView.GetNodeAt(targetPoint);
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = treeView.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = treeView.GetNodeAt(targetPoint);
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode) && targetNode.Tag is Directory)
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    (draggedNode.Parent.Tag as Directory).Content.Remove(draggedNode.Tag as AbstractFile);
                    draggedNode.Remove();

                    (targetNode.Tag as Directory).Content.Add(draggedNode.Tag as AbstractFile);
                    targetNode.Nodes.Add(draggedNode);
                }
                else if (e.Effect == DragDropEffects.Copy)
                {
                    TreeNode newNode = (TreeNode)draggedNode.Clone();

                    if (newNode.Tag is File)
                    {
                        File newFile = new File((newNode.Tag as File).Name, (newNode.Tag as File).Size);
                        newFile.Id = fileCount++;

                        files.Add(newFile);

                        newNode.Tag = newFile;

                        LocateFile(newNode.Tag as File);
                    } else if (newNode.Tag is Directory) {
                        Directory newDir = new Directory((newNode.Tag as Directory).Name);
                        newDir.Id = dirCount++;
                        newNode.Tag = newDir;

                        foreach (TreeNode node in newNode.Nodes)
                        {
                            File file = node.Tag as File;
                            File newFile = new File(file.Name, file.Size);
                            newFile.Id = fileCount++;

                            files.Add(newFile);

                            node.Tag = newFile;

                            newDir.Content.Add(newFile);

                            LocateFile(newFile);
                        }
                    }

                    targetNode.Nodes.Add(newNode);
                    (targetNode.Tag as Directory).Content.Add(newNode.Tag as AbstractFile);
                }

                targetNode.Expand();
                Draw();
            }
        }

        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            return ContainsNode(node1, node2.Parent);
        }
    }
}
