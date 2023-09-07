class IDStorage:
    def __init__(self):
        self.id_dict = {}  # Using a dictionary to store ID-strength pairs

    def add_id(self, new_id, strength):
        self.id_dict[new_id] = strength  # Adding or updating the ID-strength pair

    def get_strength(self, target_id):
        return self.id_dict.get(target_id, None)  # Get strength if ID exists, else return None

    def get_ids(self):
        return list(self.id_dict.keys())  # Get all stored IDs
