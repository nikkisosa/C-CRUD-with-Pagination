using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pagination
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private static int id;
        private static string buttons = "default";
        private static string str;
        private static int pageNumber = 1;
        private static int itemNewId = 0;
        private static int itemOldId = 0;
        private static int totalCount = 0;
        private static int totalPage = 0;
        private static Boolean onFieldDisable = false; // true if disable false if enable
        private static Boolean cancel = false; // cancel add/edit

        public void loadData()
        {
            str = Model.userInfoModel.getUserInfo(pageNumber);
            if (str == "success")
            {

                totalCount = config.records.Count;
                if (totalCount > 0)
                {
                    lvInfo.Items.Clear();
                    for (int count = 0; count < config.records.Count; count++)
                    {
                        ListViewItem item = new ListViewItem(config.records[count].id.ToString());
                        item.SubItems.Add(config.records[count].name.ToString());
                        item.SubItems.Add(config.records[count].gender.ToString());
                        lvInfo.Items.Add(item);

                        itemNewId = config.records[count].id;

                    }

                    Model.userInfoModel.getTotalCount();
                    Entity.variables variables = new Entity.variables();


                    if (itemNewId == itemOldId)
                    {
                        // no more data
                    }
                    else
                    {
                        itemOldId = itemNewId;
                        totalPage = ((config.records[0].totalCount / Entity.variables.sizePerPage) + 1);
                        lblPage.Text = "Page " + pageNumber + " of " + ((config.records[0].totalCount / Entity.variables.sizePerPage) + 1).ToString();
                    }
                }

            }
            else
            {

            }

        }

        /**
         * Enable or disable input/text fields
         */
        private void Fields()
        {
            if (onFieldDisable == true)
            {
                txtName.Enabled = true;
                cboGender.Enabled = true;
            }
            else
            {
                txtName.Enabled = false;
                cboGender.Enabled = false;
            }
        }

        /**
         * Enable and Disable Specific button
         */
        private void Buttons()
        {
            if (buttons == "default")
            {
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                btnAdd.Text = "Add User";
                btnEdit.Text = "Edit User";
                btnDelete.Text = "Delete User";
            }
            else if (buttons == "add")
            {
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Enabled = false;
                btnAdd.Text = "Save User";
                btnDelete.Text = "Cancel";
            }
            else if (buttons == "edit")
            {
                btnAdd.Enabled = false;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                btnEdit.Text = "Update User";
                btnDelete.Text = "Cancel";
            }
        }

        private void clearFields()
        {
            txtName.Clear();
            cboGender.Items.Clear();
            cboGender.Items.Add("Male");
            cboGender.Items.Add("Female");
        }


        private void frmHome_Load(object sender, EventArgs e)
        {
            Fields();
            Buttons();
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (onFieldDisable == false)
            {
                onFieldDisable = true;
                cancel = true;
                buttons = "add";
                Fields(); // enable fields
                Buttons();
            }
            else
            {
                if (onFieldDisable == true)
                {
                    string name = txtName.Text;
                    string gender = cboGender.Text;
                    if (name == "" || gender == "")
                    {
                        MessageBox.Show("Please enter name and gender", "SAMPLE CRUD", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        Entity.variables ent = new Entity.variables();
                        ent.name = name;
                        ent.gender = gender;
                        string str = Model.userInfoModel.addUserInfo(ent);
                        if (str == "success")
                        {
                            loadData();
                            MessageBox.Show("successfully added", "SAMPLE CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            onFieldDisable = false;
                            Fields();
                            buttons = "default";
                            Buttons();
                            cancel = false;
                            clearFields();
                        }
                        else
                        {
                            MessageBox.Show(str);
                        }
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                id = Convert.ToInt32(lvInfo.SelectedItems[0].Text);
                if (onFieldDisable == false)
                {
                    onFieldDisable = true;
                    cancel = true;
                    buttons = "edit";
                    Fields(); // enable fields
                    Buttons();

                    txtName.Text = lvInfo.SelectedItems[0].SubItems[1].Text;
                    cboGender.Text = lvInfo.SelectedItems[0].SubItems[2].Text;
                }
                else
                {
                    if (onFieldDisable == true)
                    {
                        string name = txtName.Text;
                        string gender = cboGender.Text;
                        if (name == "" || gender == "")
                        {
                            MessageBox.Show("Please enter the name and gender", "SAMPLE CRUD", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            Entity.variables ent = new Entity.variables();
                            ent.name = name;
                            ent.gender = gender;
                            ent.id = id;
                            string str = Model.userInfoModel.EditUserInfo(ent);
                            if (str == "success")
                            {
                                loadData();
                                MessageBox.Show("successfully update", "SAMPLE CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                onFieldDisable = false;
                                Fields();
                                buttons = "default";
                                Buttons();
                                cancel = false;
                                clearFields();
                            }
                            else
                            {
                                MessageBox.Show(str);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select the row you want to edit", "Select", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cancel == true) // will clear textbox/input fields
            {
                onFieldDisable = false;
                Fields();
                buttons = "default";
                Buttons();
                clearFields();
            }
            else // delete selected record from listview
            {
                try
                {
                    int id = Convert.ToInt32(lvInfo.SelectedItems[0].Text);
                    DialogResult dr = MessageBox.Show("Are you sure want to delete this row?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {

                        str = Model.userInfoModel.DeleteUserInfo(id);
                        if (str == "success")
                        {
                            lvInfo.Items.Remove(lvInfo.SelectedItems[0]);
                            MessageBox.Show("Successfully delete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Please select the command you want to delete", "Select", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (totalCount > 0)
            {
                if (totalPage == pageNumber)
                {

                }
                else
                {
                    pageNumber = pageNumber + 1;

                    loadData();
                }

            }
            else
            {
                //leave empty
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (totalCount != 0)
            {
                if (pageNumber == 1)
                {

                }
                else
                {
                    pageNumber = pageNumber - 1;
                    loadData();
                }

            }
            else
            {
                if (pageNumber == 1)
                {

                }
                else
                {
                    pageNumber = pageNumber - 1;
                    loadData();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want\nto exit this application?", "Are you sure", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                Environment.Exit(1);
            }
            else
            {

            }
        }
    }
}
