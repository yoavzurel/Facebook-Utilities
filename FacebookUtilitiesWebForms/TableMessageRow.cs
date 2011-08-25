using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacebookUtilitiesWebForms
{
    /// <summary>
    /// This class extends the TableRow class
    /// </summary>
    public class TableMessageRow : TableRow
    {
        public TableMessageRow(Friend i_Friend)
        {
            if (i_Friend != null)
            {
                this.Friend = i_Friend;
                this.Picture = new Image();
                this.Label = new Label();
                this.TextBox = new TextBox();
                this.ConfirmButton = new Button();
                initRow();
            }

            setRowProperties();

            // Creates the cells and adds them to the row
            populateRow(createCells());
        }

        private void populateRow(List<TableCell> i_CollectionOfCells)
        {
            // Adds all the cells inside the collection to this row
            foreach (TableCell cell in i_CollectionOfCells)
            {
                this.Cells.Add(cell);
            }
        }

        private List<TableCell> createCells()
        {
            // This collection contains all the cells
            List<TableCell> cellCollection = new List<TableCell>();

            // Picture
            TableCell pictureCell = new TableCell();
            setCellProperties(ref pictureCell);
            pictureCell.Controls.Add(this.Picture);
            cellCollection.Add(pictureCell);

            // Label for the name
            TableCell labelCell = new TableCell();
            setCellProperties(ref labelCell);
            labelCell.Controls.Add(this.Label);
            cellCollection.Add(labelCell);

            // TextBox
            TableCell textBoxCell = new TableCell();
            setCellProperties(ref textBoxCell);
            textBoxCell.Controls.Add(this.TextBox);
            cellCollection.Add(textBoxCell);

            // Confirm button
            TableCell confirmButtonCell = new TableCell();
            setCellProperties(ref confirmButtonCell);
            confirmButtonCell.Controls.Add(this.ConfirmButton);
            cellCollection.Add(confirmButtonCell);

            return cellCollection;
        }

        private void setCellProperties(ref TableCell i_Cell)
        {
            i_Cell.HorizontalAlign = HorizontalAlign.Left;
            i_Cell.VerticalAlign = VerticalAlign.Middle;
        }

        private void initRow()
        {
            this.Picture.ImageUrl = this.Friend.Pictures[ePictureTypes.pic_small.ToString()];
            this.Label.Text = this.Friend.FullName;

            setItemProperties();
        }

        private void setItemProperties()
        {
            setRowProperties();
            setLabelProperties();
            setTextBoxProperties();
            setConfirmationButtonProperties();
        }

        private void setLabelProperties()
        {
            this.Label.Attributes.Add("class", "name");
        }

        private void setTextBoxProperties()
        {
            this.TextBox.Width = 230;
            this.TextBox.Height = 60;
            this.TextBox.TextMode = TextBoxMode.MultiLine;
            this.TextBox.CssClass = "ta5";
        }

        private void setConfirmationButtonProperties()
        {
            this.ConfirmButton.CssClass = "button";
            this.ConfirmButton.Text = "Confirm";
        }

        private void setRowProperties()
        {
            this.HorizontalAlign = HorizontalAlign.Center;
            this.VerticalAlign = VerticalAlign.Middle;
        }

        public Image Picture { get; set; }

        public Friend Friend { get; set; }

        public Label Label { get; set; }

        public TextBox TextBox { get; set; }

        public Button ConfirmButton { get; set; }
    }
}